using System;

namespace Entities
{
    public sealed class EntityException : Exception
    {
        public EntityException(string message) : base(message)
        {
        }
    }
}