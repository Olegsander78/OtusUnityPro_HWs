using GameSystem;
using UnityEngine;


public sealed class BoosterFactory
{
    [GameInject]
    private MonoBehaviour monoContext;

    [GameInject]
    private IGameContext gameContext;

    public Booster CreateBooster(BoosterConfig config)
    {
        var booster = config.InstantiateBooster(this.monoContext);
        GameInjector.Inject(this.gameContext, booster);
        return booster;
    }
}