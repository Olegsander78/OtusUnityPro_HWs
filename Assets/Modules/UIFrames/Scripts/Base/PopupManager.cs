using System;
using System.Collections.Generic;

namespace UIFrames
{
    public sealed class PopupManager<TKey, TValue> : IPopupManager<TKey>, IFrame.Callback where TValue : IFrame
    {
        public event Action<TKey> OnPopupShown;

        public event Action<TKey> OnPopupHidden;

        private readonly IFrameSupplier<TKey, TValue> supplier;

        private readonly Dictionary<TKey, TValue> activePopups;

        private readonly List<TKey> cache;

        public PopupManager(IFrameSupplier<TKey, TValue> supplier)
        {
            this.supplier = supplier;
            this.activePopups = new Dictionary<TKey, TValue>();
            this.cache = new List<TKey>();
        }

        public void ShowPopup(TKey key, object args = default)
        {
            if (!this.IsPopupActive(key))
            {
                this.ShowPopupInternal(key, args);
            }
        }

        public bool IsPopupActive(TKey key)
        {
            return this.activePopups.ContainsKey(key);
        }

        public void HidePopup(TKey key)
        {
            if (this.IsPopupActive(key))
            {
                this.HidePopupInternal(key);
            }
        }

        public void HideAllPopups()
        {
            this.cache.Clear();
            this.cache.AddRange(this.activePopups.Keys);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var popupName = this.cache[i];
                this.HidePopupInternal(popupName);
            }
        }

        void IFrame.Callback.OnClose(IFrame frame)
        {
            var popup = (TValue) frame;
            if (this.TryFindName(popup, out var popupName))
            {
                this.HidePopup(popupName);
            }
        }

        private void ShowPopupInternal(TKey name, object args)
        {
            var popup = this.supplier.LoadFrame(name);
            popup.Show(args, callback: this);

            this.activePopups.Add(name, popup);
            this.OnPopupShown?.Invoke(name);
        }

        private void HidePopupInternal(TKey name)
        {
            var popup = this.activePopups[name];
            popup.Hide();

            this.activePopups.Remove(name);
            this.supplier.UnloadFrame(popup);
            this.OnPopupHidden?.Invoke(name);
        }

        private bool TryFindName(TValue popup, out TKey name)
        {
            foreach (var (key, otherPopup) in this.activePopups)
            {
                if (ReferenceEquals(popup, otherPopup))
                {
                    name = key;
                    return true;
                }
            }

            name = default;
            return false;
        }
    }
}