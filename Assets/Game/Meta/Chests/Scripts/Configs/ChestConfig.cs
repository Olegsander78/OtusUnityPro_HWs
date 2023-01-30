using System;
using UnityEngine;


public class ChestConfig : ScriptableObject
{
    [SerializeField]
    public string Id;

    [SerializeField]
    public float DurationSeconds;

    [SerializeField]
    public ChestType ChestType;

    [SerializeField]
    public int PriceOpen;

    [SerializeField]
    public ChestMetadata ChestMetadata;

    [SerializeField]
    private Reward[] _chestRewardConfigs;

    public Chest InstantiateChest(MonoBehaviour context)
    {
        return new Chest(this, context);
    }

    public ChestRewardConfig GenerateReward()
    {
        int totalDropWeight = 0;

        foreach (var dropWeigt in _chestRewardConfigs)
        {
            totalDropWeight += dropWeigt.DropChance;
        }
        Debug.Log($"{totalDropWeight} totalDropWeight!");

        int randomPoint = (int)(UnityEngine.Random.value * totalDropWeight);
        Debug.Log($"{randomPoint} randompoint!");
        for (int i = 0; i < _chestRewardConfigs.Length; i++)
        {
            if (randomPoint < _chestRewardConfigs[i].DropChance)
            {
                return _chestRewardConfigs[i].RewardConfig;
            }
            else
            {
                randomPoint -= _chestRewardConfigs[i].DropChance;
            }
        }

        return _chestRewardConfigs[_chestRewardConfigs.Length - 1].RewardConfig;
    }

    [Serializable]
    public sealed class Reward
    {
        [SerializeField]
        public ChestRewardConfig RewardConfig;

        [SerializeField]
        public int DropChance;
    }
}

//Gold: 150 - 250 , chance 40 %
//Simple Resources: 5 - 10, chance 30 %
//Experience: 50 - 100, chance 30 %

//Gold: 500 - 1000 , chance 40 %
//Simple Resources: 15 - 25, chance 30 %
//Experience: 100 - 250, chance 30 %

//Gold: 1500 - 2500 , chance 40 %
//Simple Resources: 25 - 50, chance 25 %
//Experience: 250 - 500, chance 25 %
//Crystals: 2 - 5, chance 10 %
