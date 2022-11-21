using UnityEngine;

namespace GameElements.Extensions
{
    [AddComponentMenu("GameElements/Game Element «Pause Game»")]
    public sealed class MonoGamePauseElement : MonoBehaviour, IGameAttachElement
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