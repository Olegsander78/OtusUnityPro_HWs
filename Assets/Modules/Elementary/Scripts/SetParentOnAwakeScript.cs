using UnityEngine;

namespace Elementary
{
    public sealed class SetParentOnAwakeScript : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private bool worldPositionStays = true;

        private void Awake()
        {
            this.transform.SetParent(this.parent, this.worldPositionStays);
        }
    }
}