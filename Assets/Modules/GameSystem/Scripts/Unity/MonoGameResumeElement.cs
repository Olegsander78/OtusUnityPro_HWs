using UnityEngine;

namespace GameSystem.Extensions
{
    [AddComponentMenu("GameSystem/Game Element «Resume Game»")]
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