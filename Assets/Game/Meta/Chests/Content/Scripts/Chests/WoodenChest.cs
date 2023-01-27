using GameElements;
using UnityEngine;


public sealed class WoodenChest : Chest, IGameInitElement

{    
    private HeroService _heroService;    

    public WoodenChest(WoodenChestConfig config, MonoBehaviour context) : base(config, context)
    {
               
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