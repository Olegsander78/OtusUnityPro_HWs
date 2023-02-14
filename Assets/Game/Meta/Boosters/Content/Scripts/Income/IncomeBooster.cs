using GameSystem;
using UnityEngine;


public sealed class IncomeBooster : Booster
{
    [GameInject]
    private VendorSaleInteractor vendorInteractor;

    private readonly IncomeBoosterConfig config;

    public IncomeBooster(IncomeBoosterConfig config, MonoBehaviour context) : base(config, context)
    {
        this.config = config;
    }

    protected override void OnStart()
    {
        this.vendorInteractor.IncomeMultiplier *= this.config.incomeCoefficient;
    }

    protected override void OnStop()
    {
        this.vendorInteractor.IncomeMultiplier /= this.config.incomeCoefficient;
    }
}