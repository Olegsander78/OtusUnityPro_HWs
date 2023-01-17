using UnityEngine;
using UnityEngine.Events;

namespace UIFrames.Unity
{
    [AddComponentMenu("UIFrames/Frame")]
    public class UnityFrame : MonoBehaviour, IFrame
    {
        [Space]
        [SerializeField]
        private UnityEvent<object> onShow;

        [SerializeField]
        private UnityEvent onHide;

        private IFrame.Callback callback;

        public void Show(object args, IFrame.Callback callback)
        {
            this.callback = callback;
            this.OnShow(args);
            this.onShow?.Invoke(args);
        }

        public void Hide()
        {
            this.OnHide();
            this.onHide?.Invoke();
        }
        
        public void Close()
        {
            if (this.callback != null)
            {
                this.callback.OnClose(this);
            }
        }

        protected virtual void OnShow(object args)
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}