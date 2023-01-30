using UnityEngine;


public abstract class ChestRewardConfig : ScriptableObject
{    
    public RewardMetadata RewardMetadata;

    [SerializeField]
    private int _minAmount;

    [SerializeField]
    private int _maxAmount;

    public int GenerateAmountReward()
    {
        return Random.Range(_minAmount, _maxAmount);
    }
}