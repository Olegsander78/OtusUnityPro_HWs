using System;
using UnityEngine;


public sealed class ApplicationManager : MonoBehaviour
{
    public event Action<float> OnUpdate;

    public event Action OnPaused;

    public event Action OnResumed;

    public event Action OnQuit;

    #region Lifecycle

    private void Update()
    {
        this.OnUpdate?.Invoke(Time.deltaTime);
    }

#if UNITY_EDITOR
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            this.OnResumed?.Invoke();
        }
        else
        {
            this.OnPaused?.Invoke();
        }
    }
#else
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                OnPaused?.Invoke();
            }
            else
            {
                OnResumed?.Invoke();
            }
        }
#endif

    private void OnApplicationQuit()
    {
        this.OnQuit?.Invoke();
    }

    #endregion
}