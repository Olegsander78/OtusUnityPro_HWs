using UnityEngine;

namespace GameElements.Extensions
{
    [AddComponentMenu("GameElements/Game Element «Resume Game»")]
    public sealed class MonoGameResumeElement : MonoBehaviour, IGameAttachElement
    {
        private IGameContext gameContext;

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            if (this.gameContext.State == GameState.PAUSE)
            {
                this.gameContext.ResumeGame();
            }
        }

        void IGameAttachElement.AttachGame(IGameContext context)
        {
            this.gameContext = context;
        }
    }
}