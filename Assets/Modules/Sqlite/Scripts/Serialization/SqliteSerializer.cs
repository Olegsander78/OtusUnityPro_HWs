using System;
using System.Text;

namespace SqliteModule
{
    public static class SqliteSerializer
    {
        private const char COMMA = ',';

        private const char OPENING_PARENTHESIS = '(';

        /// <summary>
        ///     <para>Converts entities to sqlite properties.</para>
        /// </summary>
        public static string Serialize<T>(T[] entities, Func<T, Params> convertToParameters)
        {
            var sb = new StringBuilder();
            var lastIndex = entities.Length - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                var serializedParameters = convertToParameters.Invoke(entities[i]);
                SerializeInternal(serializedParameters.values, sb);
                sb.Append(COMMA);
            }

            var parameters = convertToParameters.Invoke(entities[lastIndex]);
            SerializeInternal(parameters.values, sb);
            return $"{sb}";
        }

        /// <summary>
        ///     <para>Converts entity properties to sqlite parameters.</para>
        /// </summary>
        public static string Serialize(params object[] parameters)
        {
            var sb = new StringBuilder();
            SerializeInternal(parameters, sb);
            return sb.ToString();
        }

        private static void SerializeInternal(object[] parameters, StringBuilder sb)
        {
            sb.Append(OPENING_PARENTHESIS);
            var lastIndex = parameters.Length - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                sb.Append($"'{parameters[i]}',");
            }

            sb.Append($"'{parameters[lastIndex]}')");
        }
        
        public readonly struct Params
        {
            public readonly object[] values;

            public Params(params object[] values)
            {
                this.values = values;
            }
        }
    }
}