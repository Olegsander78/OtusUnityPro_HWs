using UnityEngine;

namespace GameSystem.Extensions
{
    [AddComponentMenu("GameSystem/Action «Pause Game»")]
    public sealed class GamePauseAction : MonoBehaviour, IGameAttachElement
    {
        private IGameContext gameContext;

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            if (this.gameContext.State == GameState.PLAY)
            {
                this.gameContext.PauseGame();
            }
        }

        void IGameAttachElement.AttachGame(IGameContext context)
        {
            this.gameContext = context;
        }
    }
}