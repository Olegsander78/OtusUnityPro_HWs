using System;
using UnityEditor;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Physics/Colliders Sensor «Overlap Sphere»")]
    public sealed class CollidersSensorOverlapSphere : CollidersSensorBase
    {
        [Space]
        [SerializeField]
        private Transform centerPoint;

        [SerializeField]
        private float radius;
        
        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;

        private Coroutine coroutine;

        public void SetCenterPoint(Transform centerPoint)
        {
            this.centerPoint = centerPoint;
        }

        protected override int Detect(Collider[] buffer)
        {
            return Physics.OverlapSphereNonAlloc(
                position: this.centerPoint.position,
                radius: this.radius,
                results: buffer,
                layerMask: this.layerMask,
                queryTriggerInteraction: this.triggerInteraction
            );
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