#if UNITY_EDITOR
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Game.App
{
    public static class SqliteMenu
    {
        public static void Query(string text, Action<IDbCommand> action)
        {
            var path = $"URI=file:{Application.dataPath}/Editor/tp-database.db";
            using (var connection = new SqliteConnection(path))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = text;
                    action.Invoke(command);
                }
            }
        }
    }
}
#endif
