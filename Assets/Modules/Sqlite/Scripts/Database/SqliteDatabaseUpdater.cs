using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqliteModule
{
    public class SqliteDatabaseUpdater
    {
        private readonly Dictionary<int, List<IHandler>> versionHandlers = new();

        private readonly SqliteDatabase database;

        private readonly IAdapter adapter;

        public SqliteDatabaseUpdater(SqliteDatabase database, IAdapter adapter)
        {
            this.database = database;
            this.adapter = adapter;
        }

        public async Task<Result> CheckForUpdates()
        {
            var result = new Result();

            var currentVersion = this.adapter.CurrentVersion;
            var targetVersion = this.adapter.TargetVersion;
            
            if (currentVersion >= targetVersion)
            {
                result.isSuccessful = true;
                return result;
            }

            if (this.database.IsConnected)
            {
                await this.UpdateDatabase(currentVersion, targetVersion, result);
            }

            return result;
        }

        private async Task UpdateDatabase(int currentVersion, int targetVersion, Result result)
        {
            for (var version = currentVersion + 1; version <= targetVersion; version++)
            {
                await UpdateToVersion(version, result);
                if (result.isSuccessful)
                {
                    this.adapter.CurrentVersion = version;
                }
                else
                {
                    break;
                }
            }
        }

        private async Task UpdateToVersion(int version, Result result)
        {
            if (!versionHandlers.TryGetValue(version, out var handlers))
            {
                result.isSuccessful = true;
                result.error = null;
                return;
            }

            for (int i = 0, count = handlers.Count; i < count; i++)
            {
                var handler = handlers[i];
                await handler.UpdateAsync(this.database, result);

                if (!result.isSuccessful)
                {
                    return;
                }
            }
        }

        public void RegisterHandler(int version, IHandler handler)
        {
            if (!versionHandlers.TryGetValue(version, out var handlers))
            {
                handlers = new List<IHandler>();
                versionHandlers.Add(version, handlers);
            }

            handlers.Add(handler);
        }

        public void UnregisterHandler(int version, IHandler handler)
        {
            if (versionHandlers.TryGetValue(version, out var handlers))
            {
                handlers.Remove(handler);
            }
        }
        
        public interface IAdapter
        {
            int CurrentVersion { get; set; }
            
            int TargetVersion { get; }
        }

        public interface IHandler
        {
            Task UpdateAsync(SqliteDatabase database, Result result);
        }

        public sealed class Result
        {
            public bool isSuccessful;
            
            public string error;
        }
    }
}