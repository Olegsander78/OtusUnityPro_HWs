using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Elementary
{
    public abstract class CollidersSensorBase : MonoBehaviour
    {
        public event Action OnCollidersUpdated;

        [Space]
        [SerializeField]
        private bool playOnAwake;
        
        [Space]
        [SerializeField]
        private float minScanPeriod = 0.1f;

        [SerializeField]
        private float maxScanPeriod = 0.2f;

        [Space]
        [SerializeField]
        private int bufferCapacity = 64;

        [Title("Debug")]
        [PropertyOrder(10)]
        [ReadOnly]
        [ShowInInspector]
        public bool IsPlaying
        {
            get { return this.coroutine != null; }
        }

        [PropertyOrder(11)]
        [ReadOnly]
        [ShowInInspector]
        private Collider[] buffer;

        private int bufferSize;

        private Coroutine coroutine;

        private readonly List<ICollidersSensorListener> listeners = new();

        public void GetCollidersNonAlloc(Collider[] buffer, out int size)
        {
            size = this.bufferSize;
            Array.Copy(this.buffer, buffer, size);
        }

        public void GetCollidersUnsafe(out Collider[] buffer, out int size)
        {
            buffer = this.buffer;
            size = this.bufferSize;
        }

        public void Play()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.StartCoroutine(this.UpdateColliders());
            }
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        public void AddListener(ICollidersSensorListener listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(ICollidersSensorListener listener)
        {
            this.listeners.Remove(listener);
        }

        private IEnumerator UpdateColliders()
        {
            while (true)
            {
                var period = Random.Range(this.minScanPeriod, this.maxScanPeriod);
                yield return new WaitForSeconds(period);

                Array.Clear(this.buffer, 0, this.buffer.Length);
                
                this.bufferSize = this.Detect(this.buffer);
                
                
                this.InvokeListeners(this.bufferSize, this.buffer);
                this.OnCollidersUpdated?.Invoke();
            }
        }

        private void InvokeListeners(int size, Collider[] buffer)
        {
            for (int i = 0, count = this.listeners.Count; i < count; i++)
            {
                var listener = this.listeners[i];
                listener.OnCollidersUpdated(buffer, size);
            }
        }

        protected abstract int Detect(Collider[] buffer);

        private void Awake()
        {
            this.buffer = new Collider[this.bufferCapacity];
            if (this.playOnAwake)
            {
                this.Play();
            }
        }
    }
}