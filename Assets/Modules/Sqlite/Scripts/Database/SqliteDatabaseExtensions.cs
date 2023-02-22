using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SqliteModule
{
    public static class SqliteDatabaseExtensions
    {
        public static bool SelectQuery<T>(
            this SqliteDatabase database,
            string command,
            List<T> result,
            Func<IDataReader, T> readFunc
        )
        {
            if (!database.TryCreateCommand(command, out var cmd))
            {
                return false;
            }

            result.Clear();
            using (cmd)
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = readFunc.Invoke(reader);
                        result.Add(entity);
                    }
                }
            }

            return true;
        }

        public static async Task<bool> SelectQueryAsync<T>(
            this SqliteDatabase database,
            string command,
            List<T> result,
            Func<IDataReader, T> readFunc
        )
        {
            if (!database.TryCreateCommand(command, out var cmd))
            {
                return false;
            }

            result.Clear();
            await using (cmd)
            {
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var entity = readFunc.Invoke(reader);
                        result.Add(entity);
                    }
                }
            }

            return true;
        }

        public static bool CountQuery(
            this SqliteDatabase database,
            string command,
            out int result
        )
        {
            result = default;

            if (!database.TryCreateCommand(command, out var cmd))
            {
                return false;
            }

            using (cmd)
            {
                var scalarResult = cmd.ExecuteScalar();
                result = int.Parse(scalarResult.ToString());
            }

            return true;
        }

        public static bool InsertQuery(
            this SqliteDatabase database,
            string command
        )
        {
            if (database.TryCreateCommand(command, out var cmd))
            {
                cmd.ExecuteScalar();
                return true;
            }

            return false;
        }

        public static bool UpdateEntity(
            this SqliteDatabase database,
            string command
        )
        {
            if (database.TryCreateCommand(command, out var cmd))
            {
                cmd.ExecuteScalar();
                return true;
            }

            return false;
        }

        public static bool DeleteQuery(
            this SqliteDatabase database,
            string command
        )
        {
            if (database.TryCreateCommand(command, out var cmd))
            {
                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
        }
    }
}