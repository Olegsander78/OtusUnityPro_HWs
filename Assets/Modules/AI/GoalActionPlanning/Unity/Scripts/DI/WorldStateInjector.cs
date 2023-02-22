using UnityEngine;

namespace AI.GOAP.Unity
{
    [AddComponentMenu("AI/GOAP/World State Injector")]
    public sealed class WorldStateInjector : MonoBehaviour
    {
        [SerializeField]
        private BaseWorldState worldState;

        private void Awake()
        {
            var injects = this.GetComponentsInChildren<IWorldStateInject>();
            for (int i = 0, count = injects.Length; i < count; i++)
            {
                var injection = injects[i];
                injection.WorldState = this.worldState;
            }
        }
    }
}