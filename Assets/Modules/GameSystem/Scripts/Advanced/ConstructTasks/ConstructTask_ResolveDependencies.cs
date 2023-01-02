using UnityEngine;

namespace GameSystem
{
    [CreateAssetMenu(
        fileName = "ConstructTask «Resolve Dependencies»",
        menuName = "GameElements/New ConstructTask «Resolve Dependencies»"
    )]
    public sealed class ConstructTask_ResolveDependencies : ConstructTask
    {
        [SerializeField]
        private bool injectElements = true;

        [SerializeField]
        private bool injectServices = true;
        
        public override void Construct(IGameContext gameContext)
        {
            if (this.injectElements)
            {
                GameInjector.InjectAll(gameContext, gameContext.GetAllElements());
            }

            if (this.injectServices)
            {
                GameInjector.InjectAll(gameContext, gameContext.GetAllServices());
            }
        }
    }
}