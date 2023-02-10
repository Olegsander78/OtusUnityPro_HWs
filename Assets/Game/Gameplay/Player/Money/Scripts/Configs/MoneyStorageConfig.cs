using UnityEngine;


[CreateAssetMenu(
    fileName = "MoneyConfig",
    menuName = "Gameplay/Player/New MoneyStorageConfig"
)]
public sealed class MoneyStorageConfig : ScriptableObject
{
    public const string CONFIG_PATH = "PlayerMoneyConfig";

    public int InitialMoney
    {
        get { return _initialMoney; }
    }

    [SerializeField]
    private int _initialMoney;

    public static MoneyStorageConfig LoadAsset()
    {
        return Resources.Load<MoneyStorageConfig>(CONFIG_PATH);
    }
}