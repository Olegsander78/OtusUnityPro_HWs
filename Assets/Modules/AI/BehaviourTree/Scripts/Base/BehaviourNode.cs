#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace AI.BTree
{
    public abstract class BehaviourNode : IBehaviourNode
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, PropertyOrder(-1000)]
#endif
        public bool IsRunning
        {
            get { return this.isRunning; }
        }

        private bool isRunning;

        private IBehaviourCallback callback;

        public void Run(IBehaviourCallback callback)
        {
            if (this.isRunning)
            {
                return;
            }

            this.callback = callback;
            this.isRunning = true;
            this.Run();
        }

        public void Abort()
        {
            if (!this.isRunning)
            {
                return;
            }

            this.OnAbort();
            this.isRunning = false;
            this.callback = null;
            this.OnEnd();
        }

        protected abstract void Run();

        protected void Return(bool success)
        {
            if (!this.isRunning)
            {
                return;
            }

            this.isRunning = false;
            this.OnReturn(success);
            this.OnEnd();
            this.InvokeCallback(success);
        }

        #region Callbacks

        protected virtual void OnReturn(bool success)
        {
        }

        protected virtual void OnAbort()
        {
        }

        protected virtual void OnEnd()
        {
        }

        #endregion

        private void InvokeCallback(bool success)
        {
            if (this.callback == null)
            {
                return;
            }

            var callback = this.callback;
            this.callback = null;
            callback.Invoke(this, success);
        }
    }
}