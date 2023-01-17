using UnityEngine;


[CreateAssetMenu(
    fileName = "MeleeDamageUpgradeConfig",
    menuName = UpgradeExtensions.MENU_PATH + "New MeleeDamageUpgradeConfig"
)]
public sealed class MeleeDamageUpgradeConfig : UpgradeConfig
{
    [SerializeField]
    public MeleeDamageUpgradeTable MeleeDamageTable;

    public override Upgrade InstantiateUpgrade()
    {
        return new MeleeDamageUpgrade(this);
    }

    protected override void Validate()
    {
        base.Validate();
        MeleeDamageTable.OnValidate(MaxLevel);
    }
}