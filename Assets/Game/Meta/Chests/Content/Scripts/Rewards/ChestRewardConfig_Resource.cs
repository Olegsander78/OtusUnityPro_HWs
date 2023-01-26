using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(
    fileName = "Chest Reward - Resources",
    menuName = "Meta/Chests/New Reward (Resources)"
)]
public class ChestRewardConfig_Resource : ChestRewardConfig
{
    [ShowInInspector, ReadOnly]
    public ResourceType ResourceType
    {
        get
        {
            var rnd = new Random();
            return (ResourceType)rnd.Next(Enum.GetNames(typeof(ResourceType)).Length);
        }
    }    


    //[ReadOnly]
    //public int AmountResource
    //{
    //    get { return Random.Range(minAmountResource, maxAmountResource); }
    //}

    //[SerializeField]
    //private int minAmountResource;

    //[SerializeField]
    //private int maxAmountResource;
}