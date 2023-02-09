using System.Collections.Generic;

namespace GameSystem
{
    public interface IGameServiceGroup
    {
        IEnumerable<object> GetServices();
    }

    public sealed class GameServiceGroup : IGameServiceGroup
    {
        private readonly IEnumerable<object> services;

        public GameServiceGroup(IEnumerable<object> services)
        {
            this.services = services;
        }

        public IEnumerable<object> GetServices()
        {
            return this.services;
        }
    }
}