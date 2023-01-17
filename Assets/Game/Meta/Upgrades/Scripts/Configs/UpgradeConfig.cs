using System;
using UnityEngine;


public abstract class UpgradeConfig : ScriptableObject
{
    protected const float SPACE_HEIGHT = 10.0f;

    [SerializeField]
    public string Id;

    [Range(1, 100)]
    [SerializeField]
    public int MaxLevel = 1;

    [Space(SPACE_HEIGHT)]
    [SerializeField]
    public UpgradeMetadata Metadata;

    [SerializeField]
    public UpgradePriceTable PriceTable;

    public abstract Upgrade InstantiateUpgrade();

    private void OnValidate()
    {
        try
        {
            Validate();
        }
        catch (Exception)
        {
            // ignored
        }
    }
    protected virtual void Validate()
    {
        PriceTable.OnValidate(this.MaxLevel);
    }
}