using System;
using Entities;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class VendorSaleInteractor
{
    public event Action<VendorSaleResult> OnResourcesSold;

    [ReadOnly]
    [ShowInInspector]
    public float IncomeMultiplier { get; set; } = 1;

    private ResourceStorage resourceStorage;

    private MoneyStorage moneyStorage;

    //private MoneyPanelAnimator_AddMoney moneyAnimator;

    [SerializeField]
    private AudioClip saleSFX;

    [GameInject]
    public void Construct(
        ResourceStorage resourceStorage,
        MoneyStorage moneyStorage
        //MoneyPanelAnimator_AddMoney moneyAnimator
    )
    {
        this.resourceStorage = resourceStorage;
        this.moneyStorage = moneyStorage;
        //this.moneyAnimator = moneyAnimator;
    }

    public void SaleResources(IEntity vendor)
    {
        //var vendorInfo = vendor.Get<IComponent_Info>();
        //var resourceType = vendorInfo.ResourceType;

        //var amount = this.resourceStorage.GetResource(resourceType);
        //if (amount <= 0)
        //{
        //    return;
        //}

        //var price = vendorInfo.PricePerOne;
        //this.resourceStorage.ExtractResource(resourceType, amount);

        //var income = Mathf.RoundToInt(price * amount * this.IncomeMultiplier);
        //this.moneyStorage.EarnMoney(income);

        //var result = new VendorSaleResult
        //{
        //    vendor = vendor,
        //    resourceType = resourceType,
        //    resourceAmount = amount,
        //    moneyIncome = income
        //};

        //this.PlayParticlesToUI(result, income);
        //this.InteractWithVendor(vendor);
        //this.PlaySound();

        //this.OnResourcesSold?.Invoke(result);
    }

    private void InteractWithVendor(IEntity vendor)
    {
        //if (vendor.TryGet(out IComponent_CompleteDeal component))
        //{
        //    component.NotifyAboutCompleted();
        //}
    }

    private void PlayParticlesToUI(VendorSaleResult result, int income)
    {
        //var emissionPosition = result.vendor.Get<IComponent_GetParticlePosition>().Position;
        //this.moneyAnimator.PlayIncomeFromWorld(emissionPosition, income);
    }

    private void PlaySound()
    {
        GameAudioManager.PlaySound(GameAudioType.INTERFACE, this.saleSFX);
    }
}