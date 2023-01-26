using GameElements;
using UnityEngine;


public sealed class WoodenChest : Chest, IGameInitElement

{    
    private HeroService _heroService;

    //private readonly IEffect speedEffect;

    public WoodenChest(WoodenChestConfig config, MonoBehaviour context) : base(config, context)
    {
        //this.speedEffect = new Effect(
        //    new FloatEffectParameter(EffectParameterKey.MOVE_SPEED, config.speedMultiplier)
        //);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _heroService = context.GetService<HeroService>();
    }

    protected override void OnStart()
    {
        var hero = _heroService.GetHero();
        //_hero.Get<IComponent_Effector>().AddEffect(this.speedEffect);
    }

    protected override void OnStop()
    {
        var hero = _heroService.GetHero();
        //hero.Get<IComponent_Effector>().RemoveEffect(this.speedEffect);
    }


}