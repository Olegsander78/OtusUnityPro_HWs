using UnityEngine;

public class Component_EnemyInfo : MonoBehaviour,
    IComponent_GetEnemyType,
    IComponent_GetExpRewarded,
    IComponent_GetMoneyRewarded
{
    public EnemyType EnemyType => _enemyInfo.EnemyType;

    public int ExpReward => Random.Range(_enemyInfo.MinExperience, _enemyInfo.MaxExperience);

    public int MoneyReward => Random.Range(_enemyInfo.MinMoney, _enemyInfo.MaxMoney);


    [SerializeField]
    private ScriptableEnemyInfo _enemyInfo;
}
