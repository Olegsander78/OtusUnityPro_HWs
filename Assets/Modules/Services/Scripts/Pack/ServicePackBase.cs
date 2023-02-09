using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public abstract class ServicePackBase : ScriptableObject
    {
        public abstract IEnumerable<object> ProvideServices();
    }
}