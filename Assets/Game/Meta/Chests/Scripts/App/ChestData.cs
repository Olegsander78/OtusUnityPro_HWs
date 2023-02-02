using System;
using UnityEngine;


[Serializable]
public struct ChestData
{
    [SerializeField]
    public string id;

    [SerializeField]
    public float remainingTime;
}