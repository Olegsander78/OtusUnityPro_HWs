using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Elementary
{
    public sealed class ColliderSensor : MonoBehaviour
    {
        public event Action OnCollidersUpdated;

        [Space]
        [SerializeField]
        private Transform centerPoint;

        [SerializeField]
        private float radius;

        [Space]
        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;

        [Space]
        [SerializeField]
        private float minScanPeriod = 0.1f;

        [SerializeField]
        private float maxScanPeriod = 0.2f;

        [Space]
        [SerializeField]
        private int bufferCapacity = 64;

        [Space]
        [SerializeField]
        private bool playOnAwake;

        [Title("Debug")]
        [PropertyOrder(10)]
        [ReadOnly]
        [ShowInInspector]
        public bool IsActive
        {
            get { return this.coroutine != null; }
        }

        [PropertyOrder(11)]
        [ReadOnly]
        [ShowInInspector]
        private Collider[] buffer;

        private int bufferSize;

        private Coroutine coroutine;

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

        private IEnumerator UpdateColliders()
        {
            while (true)
            {
                
                var period = Random.Range(this.minScanPeriod, this.maxScanPeriod);
                yield return new WaitForSeconds(period);

                Array.Clear(this.buffer, 0, this.buffer.Length);
                
                this.bufferSize = Physics.OverlapSphereNonAlloc(
                    position: this.centerPoint.position,
                    radius: this.radius,
                    results: this.buffer,
                    layerMask: this.layerMask,
                    queryTriggerInteraction: this.triggerInteraction
                );

                this.OnCollidersUpdated?.Invoke();
            }
        }

        private void Awake()
        {
            this.buffer = new Collider[this.bufferCapacity];
            if (this.playOnAwake)
            {
                this.Play();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            try
            {
                var prevColor = Handles.color;
                Handles.color = Color.blue;
                Handles.DrawWireDisc(this.centerPoint.position, Vector3.up, this.radius, 1.25f);
                Handles.color = prevColor;
            }
            catch (Exception)
            {
            }
        }
#endif
    }
}