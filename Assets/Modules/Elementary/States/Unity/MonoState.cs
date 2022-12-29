using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/State")]
    public class MonoState : MonoBehaviour
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}