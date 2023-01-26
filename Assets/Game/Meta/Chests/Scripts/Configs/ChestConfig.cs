using UnityEngine;


public abstract class ChestConfig : ScriptableObject
{
    [SerializeField]
    public string id;

    [SerializeField]
    public float durationSeconds;

    [SerializeField]
    public int chestRewards;

    public abstract Chest InstantiateChest(MonoBehaviour context);
}