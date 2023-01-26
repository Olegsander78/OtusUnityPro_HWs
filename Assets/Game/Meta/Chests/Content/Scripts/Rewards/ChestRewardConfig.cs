using Sirenix.OdinInspector;
using UnityEngine;


public abstract class ChestRewardConfig : ScriptableObject
{
    [SerializeField]
    public string Id;

    [SerializeField]
    public string DisplayName;

    [SerializeField]
    public string Description;

    [ShowInInspector, ReadOnly]
    public int Amount
    {
        get { return Random.Range(minAmount, maxAmount); }
    }

    [SerializeField]
    private int minAmount;

    [SerializeField]
    private int maxAmount;
}