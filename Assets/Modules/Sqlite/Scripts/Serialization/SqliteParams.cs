namespace SqliteModule
{
    public readonly struct SqliteParams
    {
        public readonly object[] values;

        public SqliteParams(params object[] values)
        {
            this.values = values;
        }
    }
}