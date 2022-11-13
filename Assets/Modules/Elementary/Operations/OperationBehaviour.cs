using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Elementary
{
    public abstract class OperationBehaviour<T> : MonoBehaviour
    {
        public event Action<T> OnOperationStarted;

        public event Action<T> OnOperationCanceled;

        public event Action<T> OnOperationFinished;

        [PropertyOrder(-10)]
        [ReadOnly]
        [ShowInInspector]
        public bool IsStarted { get; private set; }

        [PropertyOrder(-10)]
        [ReadOnly]
        [ShowInInspector]
        public T CurrentOperation { get; private set; }

        public void StartOperation(T operation)
        {
            if (this.IsStarted)
            {
                Debug.LogWarning("Operation is already started!");
                return;
            }

            this.IsStarted = true;
            this.CurrentOperation = operation;
            this.OnOperationStarted?.Invoke(operation);
        }

        public void FinishOperation()
        {
            if (!this.IsStarted)
            {
                Debug.LogWarning("Operation is not started!");
                return;
            }

            this.IsStarted = false;
            var operation = this.CurrentOperation;
            this.CurrentOperation = default;

            this.OnOperationFinished?.Invoke(operation);
        }

        public void CancelOperation()
        {
            if (!this.IsStarted)
            {
                Debug.LogWarning("Operation is not started!");
                return;
            }

            this.IsStarted = false;
            var operation = this.CurrentOperation;
            this.CurrentOperation = default;

            this.OnOperationCanceled?.Invoke(operation);
        }
    }
}