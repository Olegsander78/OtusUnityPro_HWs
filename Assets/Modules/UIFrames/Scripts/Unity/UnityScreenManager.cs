using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UIFrames.Unity
{
    public abstract class UnityScreenManager<T> : MonoBehaviour, IScreenManager<T>
    {
        public event Action<T> OnScreenShown;

        public event Action<T> OnScrenHidden;

        public event Action<T> OnScreenChanged;

        protected abstract IFrameSupplier<T, UnityFrame> Supplier { get; }

        private IScreenManager<T> manager;

        protected virtual void Awake()
        {
            this.manager = new ScreenManager<T, UnityFrame>(this.Supplier);
        }

        protected virtual void OnEnable()
        {
            this.manager.OnScreenShown += this.OnShowScreen;
            this.manager.OnScreenChanged += this.OnChangeScreen;
            this.manager.OnScrenHidden += this.OnHideScreen;
        }

        protected virtual void OnDisable()
        {
            this.manager.OnScreenShown -= this.OnShowScreen;
            this.manager.OnScreenChanged -= this.OnChangeScreen;
            this.manager.OnScrenHidden -= this.OnHideScreen;
        }

        [Button]
        public void ChangeScreen(T key, object args = default)
        {
            this.manager.ChangeScreen(key, args);
        }

        public bool IsScreenActive(T key)
        {
            return this.manager.IsScreenActive(key);
        }

        private void OnShowScreen(T screenName)
        {
            this.OnScreenShown?.Invoke(screenName);
        }

        private void OnHideScreen(T screenName)
        {
            this.OnScrenHidden?.Invoke(screenName);
        }

        private void OnChangeScreen(T screenName)
        {
            this.OnScreenChanged?.Invoke(screenName);
        }
    }
}