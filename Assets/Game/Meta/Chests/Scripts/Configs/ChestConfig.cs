using UnityEngine;


public abstract class ChestConfig : ScriptableObject
{
    [SerializeField]
    public string Id;

    [SerializeField]
    public float DurationSeconds;

    [SerializeField]
    public ChestMetadata ChestMetadata;

    [SerializeField]
    public ChestRewardConfig[] ChestRewardConfigs;

    public abstract Chest InstantiateChest(MonoBehaviour context);
}