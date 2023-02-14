using UnityEngine;


[CreateAssetMenu(
    fileName = "IncomeBoosterConfig",
    menuName = BoosterExtensions.MENU_PATH + "New IncomeBoosterConfig"
)]
public sealed class IncomeBoosterConfig : BoosterConfig
{
    [Space]
    [SerializeField]
    public float incomeCoefficient = 2.0f;

    public override Booster InstantiateBooster(MonoBehaviour context)
    {
        return new IncomeBooster(this, context);
    }
}