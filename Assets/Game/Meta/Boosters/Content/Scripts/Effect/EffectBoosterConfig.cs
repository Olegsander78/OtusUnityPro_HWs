using Game.GameEngine.Mechanics;
using UnityEngine;


[CreateAssetMenu(
    fileName = "EffectBoosterConfig",
    menuName = BoosterExtensions.MENU_PATH + "New EffectBoosterConfig"
)]
public sealed class EffectBoosterConfig : BoosterConfig
{
    [Space]
    [SerializeField]
    public IEffect effect = new Effect();

    public override Booster InstantiateBooster(MonoBehaviour context)
    {
        return new EffectBooster(this, context);
    }
}