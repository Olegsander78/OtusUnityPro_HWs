using UnityEngine;


[CreateAssetMenu(
    fileName = "HitPointsUpgradeConfig",
    menuName = UpgradeExtensions.MENU_PATH + "New HitPointsUpgradeConfig"
)]
public sealed class HitPointsUpgradeConfig : UpgradeConfig
{
    [SerializeField]
    public HitPointsUpgradeTable HitPointsTable;

    public override Upgrade InstantiateUpgrade()
    {
        return new HitPointsUpgrade(this);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        HitPointsTable.OnValidate(maxLevel);
    }
}