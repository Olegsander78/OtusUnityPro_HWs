using UnityEngine;
using UnityEngine.Events;

namespace GameSystem.Extensions
{
    public sealed class GameFinishObserver_InvokeUnityEvent : MonoBehaviour, IGameFinishElement
    {
        [SerializeField]
        private UnityEvent onFinishGame;

        void IGameFinishElement.FinishGame()
        {
            this.onFinishGame?.Invoke();
        }
    }
}