using UnityEngine;
using UnityEngine.Events;

namespace GameSystem.Extensions
{
    public sealed class GameStartObserver_InvokeUnityEvent : MonoBehaviour, IGameStartElement
    {
        [SerializeField]
        private UnityEvent onStartGame;
       
        void IGameStartElement.StartGame()
        {
            this.onStartGame?.Invoke();
        }
    }
}