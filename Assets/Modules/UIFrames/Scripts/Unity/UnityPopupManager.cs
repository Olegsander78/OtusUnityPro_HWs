using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UIFrames.Unity
{
    public abstract class UnityPopupManager<T> : MonoBehaviour, IPopupManager<T>
    {
        public event Action<T> OnPopupShown;

        public event Action<T> OnPopupHidden;
        
        protected abstract IFrameSupplier<T, UnityFrame> Supplier { get; }

        private IPopupManager<T> manager;

        protected virtual void Awake()
        {
            this.manager = new PopupManager<T, UnityFrame>(this.Supplier);
        }

        protected virtual void OnEnable()
        {
            this.manager.OnPopupShown += this.OnShowPopup;
            this.manager.OnPopupHidden += this.OnHidePopup;
        }

        protected virtual void OnDisable()
        {
            this.manager.OnPopupShown -= this.OnShowPopup;
            this.manager.OnPopupHidden -= this.OnHidePopup;
        }

        [Button]
        public void ShowPopup(T key, object args = null)
        {
            this.manager.ShowPopup(key, args);
        }

        [Button]
        public void HidePopup(T key)
        {
            this.manager.HidePopup(key);
        }

        [Button]
        public void HideAllPopups()
        {
            this.manager.HideAllPopups();
        }

        public bool IsPopupActive(T popupName)
        {
            return this.manager.IsPopupActive(popupName);
        }

        private void OnShowPopup(T popupName)
        {
            this.OnPopupShown?.Invoke(popupName);
        }

        private void OnHidePopup(T popupName)
        {
            this.OnPopupHidden?.Invoke(popupName);
        }
    }
}