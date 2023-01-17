using UnityEngine;


[CreateAssetMenu(
    fileName = "SpeedUpgradeConfig",
    menuName = UpgradeExtensions.MENU_PATH + "New SpeedUpgradeConfig"
)]
public sealed class SpeedUpgradeConfig : UpgradeConfig
{
    [SerializeField]
    public SpeedUpgradeTable SpeedTable;

    public override Upgrade InstantiateUpgrade()
    {
        return new SpeedUpgrade(this);
    }

    protected override void Validate()
    {
        base.Validate();
        SpeedTable.OnValidate(MaxLevel);
    }
}