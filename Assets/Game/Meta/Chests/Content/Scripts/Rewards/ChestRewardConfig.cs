using UnityEngine;


public abstract class ChestRewardConfig : ScriptableObject
{
    [SerializeField]
    private RewardMetadata _rewardMetadata;

    [SerializeField]
    private int _minAmount;

    [SerializeField]
    private int _maxAmount;

    public int GenerateAmount()
    {
        return Random.Range(_minAmount, _maxAmount);
    }
}