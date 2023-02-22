using System;
using UnityEngine;

namespace AI.BTree
{
    [AddComponentMenu(Extensions.MENU_PATH + "Behaviour Tree")]
    public sealed class UnityBehaviourTree : UnityBehaviourNode,
        IBehaviourTree,
        IBehaviourCallback
    {
        public event Action OnStarted;
    
        public event Action<bool> OnFinished;

        public event Action OnAborted;

        public bool IsEnable
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        [SerializeField]
        private bool autoRun = true;

        [SerializeField]
        private bool loop = true;

        [SerializeField]
        private UpdateMode updateMode;

        [SerializeField]
        private UnityBehaviourNode root;

        private void Start()
        {
            if (this.autoRun)
            {
                this.Run();
            }
        }

        private void Update()
        {
            if (this.loop && this.updateMode == UpdateMode.UPDATE)
            {
                this.Run();
            }
        }

        private void FixedUpdate()
        {
            if (this.loop && this.updateMode == UpdateMode.FIXED_UPDATE)
            {
                this.Run();
            }
        }

        private void LateUpdate()
        {
            if (this.loop && this.updateMode == UpdateMode.LATE_UPDATE)
            {
                this.Run();
            }
        }

        protected override void Run()
        {
            if (!this.root.IsRunning)
            {
                this.OnStarted?.Invoke();
                this.root.Run(callback: this);
            }
        }

        protected override void OnAbort()
        {
            if (this.IsRunning)
            {
                this.root.Abort();
                this.OnAborted?.Invoke();
            }
        }
        void IBehaviourCallback.Invoke(IBehaviourNode node, bool success)
        {
            this.Return(success);
            this.OnFinished?.Invoke(success);
        }

        private enum UpdateMode
        {
            UPDATE = 0,
            FIXED_UPDATE = 1,
            LATE_UPDATE = 2
        }

#if UNITY_EDITOR
        public UnityBehaviourNode Editor_GetRoot()
        {
            return this.root;
        }
#endif
    }
}