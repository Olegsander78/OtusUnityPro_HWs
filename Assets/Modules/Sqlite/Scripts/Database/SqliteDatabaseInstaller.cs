using System.IO;

#if !UNITY_EDITOR
using UnityEngine.Networking;
#endif

namespace SqliteModule
{
    public class SqliteDatabaseInstaller
    {
        private readonly IAdapter adapter;

        public SqliteDatabaseInstaller(IAdapter adapter)
        {
            this.adapter = adapter;
        }

        public void Install()
        {
            var sourcePath = this.adapter.SourceFilePath;
            var destinationPath = this.adapter.DestinationFilePath;
            
            if (this.adapter.IsInstalled)
            {
                if (!File.Exists(destinationPath))
                {
                     Install(sourcePath, destinationPath);
                }
            }
            else
            {
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                Install(sourcePath, destinationPath);
                this.adapter.IsInstalled = true;
            }
        }

        private static void Install(string sourcePath, string destinationPath)
        {
#if UNITY_EDITOR
            File.Copy(sourcePath, destinationPath);
#elif UNITY_ANDROID || UNITY_IOS
            var request = UnityWebRequest.Get(sourcePath);
            var asyncOperation = request.SendWebRequest();
            while (!asyncOperation.isDone)
            {
            }
            
            var bytes = request.downloadHandler.data;
            File.WriteAllBytes(destinationPath, bytes);
#endif
        }
        
        public interface IAdapter
        {
            /// <summary>
            ///     <para>Is database already installed.</para>
            /// </summary>
            bool IsInstalled { get; set; }
            
            /// <summary>
            ///     <para>Origin database file path.</para>
            /// </summary>
            string SourceFilePath { get; }
            
            /// <summary>
            ///     <para>Target database file path.</para>
            /// </summary>
            string DestinationFilePath { get; }
        }
    }
}