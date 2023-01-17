using System.Collections.Generic;
using UnityEngine;

namespace UIFrames.Unity
{
    public abstract class UnityFrameSupplier<K, V> : MonoBehaviour, IFrameSupplier<K, V> where V : UnityFrame
    {
        private readonly Dictionary<K, V> cashedFrames;

        public UnityFrameSupplier()
        {
            this.cashedFrames = new Dictionary<K, V>();
        }

        public V LoadFrame(K key)
        {
            if (this.cashedFrames.TryGetValue(key, out var frame))
            {
                frame.gameObject.SetActive(true);
            }
            else
            {
                frame = this.InstantiateFrame(key);
                this.cashedFrames.Add(key, frame);
            }

            frame.transform.SetAsLastSibling();
            return frame;
        }

        public void UnloadFrame(V frame)
        {
            frame.gameObject.SetActive(false);
        }

        protected abstract V InstantiateFrame(K key);
    }
}