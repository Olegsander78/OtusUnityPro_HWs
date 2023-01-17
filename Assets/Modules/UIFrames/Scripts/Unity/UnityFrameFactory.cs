using UnityEngine;

namespace UIFrames.Unity
{
    public abstract class UnityFrameFactory<K, V> : MonoBehaviour, IFrameFactory<K, V> where V : UnityFrame
    {
        [SerializeField]
        private Transform container;

        public V CreateFrame(K key)
        {
            var prefab = this.GetPrefab(key);
            var popup = Instantiate(prefab, this.container);
            this.OnFrameCreated(popup);
            return popup;
        }

        protected abstract V GetPrefab(K key);

        protected virtual void OnFrameCreated(V popup)
        {
        }
    }
}