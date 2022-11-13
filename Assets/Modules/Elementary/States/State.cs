using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/State")]
    public class State : MonoBehaviour, IState
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}