using UnityEngine;


public abstract class UpgradeConfig : ScriptableObject
{
    [SerializeField]
    public string id;

    [SerializeField]
    public int maxLevel;

    [SerializeField]
    public UpgradePriceTable priceTable;

    public abstract Upgrade InstantiateUpgrade();

    protected virtual void OnValidate()
    {
        this.priceTable.OnValidate(this.maxLevel);
    }
}