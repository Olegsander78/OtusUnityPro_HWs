using UnityEngine;

public class Component_EnemyInfo : MonoBehaviour,
    IComponent_ExpRewarded,
    IComponent_MoneyRewarded,
    IComponent_GetEnemyInfo,
    IComponent_GetEnemyType
{
    public ScriptableEnemyInfo EnemyInfo => _enemyInfo;

    public int ExpReward
    {
        get => _expReward;
        set => _expReward = value;
    }

    public int MoneyReward
    {
        get => _moneyReward;
        set => _moneyReward = value;
    }

    EnemyType IComponent_GetEnemyType.EnemyType => _enemyInfo.EnemyType;

    [SerializeField]
    private ScriptableEnemyInfo _enemyInfo;

    private int _expReward;

    private int _moneyReward;

    private void Awake()
    {
        _expReward = Random.Range(_enemyInfo.MinExperience, _enemyInfo.MaxExperience);
        _moneyReward = Random.Range(_enemyInfo.MinMoney, _enemyInfo.MaxMoney);
    }
}
