using System;

namespace UIFrames
{
    public sealed class ScreenManager<TKey, TValue> : IScreenManager<TKey> where TValue : IFrame
    {
        public event Action<TKey> OnScreenShown;

        public event Action<TKey> OnScrenHidden;

        public event Action<TKey> OnScreenChanged;

        private readonly IFrameSupplier<TKey, TValue> supplier;

        private TKey currentScreenKey;

        private TValue currentScreen;

        #region Lifecycle

        public ScreenManager(IFrameSupplier<TKey, TValue> supplier)
        {
            this.supplier = supplier;
        }

        #endregion

        public bool IsScreenActive(TKey key)
        {
            return ReferenceEquals(this.currentScreenKey, key);
        }

        public void ChangeScreen(TKey key, object args = default)
        {
            if (!ReferenceEquals(this.currentScreen, null))
            {
                this.HideScreenInternal(this.currentScreenKey, this.currentScreen);
            }

            this.currentScreenKey = key;
            this.ShowScreenInternal(key, args);
            this.OnScreenChanged?.Invoke(key);
        }

        private void ShowScreenInternal(TKey key, object args)
        {
            this.currentScreen = this.supplier.LoadFrame(key);
            this.currentScreen.Show(args);
            this.OnScreenShown?.Invoke(key);
        }

        private void HideScreenInternal(TKey key, TValue screen)
        {
            screen.Hide();
            this.supplier.UnloadFrame(screen);
            this.OnScrenHidden?.Invoke(key);
        }
    }
}