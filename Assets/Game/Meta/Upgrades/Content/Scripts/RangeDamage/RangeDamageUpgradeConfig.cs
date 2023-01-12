using UnityEngine;


[CreateAssetMenu(
    fileName = "RangeDamageUpgradeConfig",
    menuName = UpgradeExtensions.MENU_PATH + "New RangeDamageUpgradeConfig"
)]
public sealed class RangeDamageUpgradeConfig : UpgradeConfig
{
    [SerializeField]
    public RangeDamageUpgradeTable RangeDamageTable;

    public override Upgrade InstantiateUpgrade()
    {
        return new RangeDamageUpgrade(this);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        RangeDamageTable.OnValidate(maxLevel);
    }
}