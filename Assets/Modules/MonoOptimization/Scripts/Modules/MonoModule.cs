using System.Collections.Generic;
using UnityEngine;

namespace MonoOptimization
{
    public abstract class MonoModule : MonoBehaviour
    {
        public virtual IEnumerable<IMonoComponent> ProvideMonoComponents()
        {
            yield break;
        }
        
        public virtual void ConstructSensor(MonoContextModular context)
        {
        }
    }
}