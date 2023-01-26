using System;
using UnityEngine;


public abstract class ChestConfig : ScriptableObject
{
    [Serializable]
    public class ChestRewardWithDropChance
    {
        [SerializeField]
        public ChestRewardConfig RewardConfig;

        [SerializeField]
        public int DropChance;
    }

    [SerializeField]
    public string Id;

    [SerializeField]
    public float DurationSeconds;

    [SerializeField]
    public ChestMetadata ChestMetadata;

    [SerializeField]
    public ChestRewardWithDropChance[] ChestRewardConfigs;

    public abstract Chest InstantiateChest(MonoBehaviour context);    
}