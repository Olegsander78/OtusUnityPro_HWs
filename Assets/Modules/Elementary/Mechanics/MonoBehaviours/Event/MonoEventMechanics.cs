using UnityEngine;

namespace Elementary
{
    public abstract class MonoEventMechanics : MonoBehaviour
    {
        [SerializeField]
        private MonoEmitter receiver;

        protected virtual void OnEnable()
        {
            this.receiver.OnEvent += this.OnEvent;
        }

        protected virtual  void OnDisable()
        {
            this.receiver.OnEvent -= this.OnEvent;
        }

        protected abstract void OnEvent();
    }
}