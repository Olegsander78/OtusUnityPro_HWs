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

    //protected override ChestRewardConfig GetReward()
    //{
        //int totalDropWeight = 0;

        //foreach (var dropWeigt in  this.Config.ChestRewardConfigs)
        //{
        //    totalDropWeight += dropWeigt.DropChance;
        //}

        //int randomPoint = (int)Random.value * totalDropWeight;

        //for (int i = 0; i < this.Config.ChestRewardConfigs.Length; i++)
        //{
        //    if(randomPoint < this.Config.ChestRewardConfigs[i].DropChance)
        //    {
        //        return this.Config.ChestRewardConfigs[i].RewardConfig;
        //    }
        //    else
        //    {
        //        randomPoint -= this.Config.ChestRewardConfigs[i].DropChance;
        //    }            
        //}

        //return this.Config.ChestRewardConfigs[this.Config.ChestRewardConfigs.Length - 1].RewardConfig;

        //float Choose(float[] probs)
        //{

        //float total = 0;

        //foreach (float elem in probs)
        //{
        //    total += elem;
        //}

        //float randomPoint = Random.value * total;

        //for (int i = 0; i < probs.Length; i++)
        //{
        //    if (randomPoint < probs[i])
        //    {
        //        return i;
        //    }
        //    else
        //    {
        //        randomPoint -= probs[i];
        //    }
        //}
        //return probs.Length - 1;
        //}
    //}
}