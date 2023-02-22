using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Mono.Data.Sqlite;
using UnityEngine;

#pragma warning disable 618

namespace SqliteModule
{
    public class SqliteDatabase
    {
        public event Action OnConnected;

        public event Action OnConnectFailed;

        public event Action OnDisconnected;

        public bool IsConnected
        {
            get { return connection != null; }
        }

        private readonly string connectionUri;
        
        private DbConnection connection;

        public SqliteDatabase(string uri)
        {
            connectionUri = uri;
        }

        public async Task<bool> ConnectAsync()
        {
            if (IsConnected)
            {
                Debug.LogWarning("Database is already connected!");
                return false;
            }

            connection = new SqliteConnection(connectionUri);
            await connection.OpenAsync();

            if (connection.State != ConnectionState.Open)
            {
                Debug.LogWarning("Database connection is failed!");
                OnConnectFailed?.Invoke();
                return false;
            }

            OnConnected?.Invoke();
            return true;
        }

        public bool Connect()
        {
            if (IsConnected)
            {
                Debug.LogWarning("Database is already connected!");
                return false;
            }

            connection = new SqliteConnection(connectionUri);
            connection.Open();

            if (connection.State != ConnectionState.Open)
            {
                Debug.LogWarning("Database connection is failed!");
                OnConnectFailed?.Invoke();
                return false;
            }

            OnConnected?.Invoke();
            return true;
        }

        public async Task DisconnectAsync()
        {
            if (!IsConnected)
            {
                return;
            }

            var connection = this.connection;
            this.connection = null;

            await connection.CloseAsync();
            connection.Dispose();
            OnDisconnected?.Invoke();
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }

            var connection = this.connection;
            this.connection = null;

            connection.Close();
            connection.Dispose();

            OnDisconnected?.Invoke();
        }

        public bool TryCreateCommand(string commandText, out DbCommand command)
        {
            if (connection.State != ConnectionState.Open)
            {
                command = null;
                return false;
            }

            command = connection.CreateCommand();
            command.CommandText = commandText;
            return true;
        }

        public DbCommand CreateCommand(string commandText)
        {
            if (connection.State != ConnectionState.Open)
            {
                throw new Exception($"Can not execute command {commandText} " +
                                    $"with connection state: {connection.State}");
            }

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
    }
}