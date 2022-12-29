using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "ScriptableResourceObject",
    menuName = "Gameplay/Enemies/New ScriptableEnemyInfo"
)]
public sealed class ScriptableEnemyInfo : ScriptableObject
{
    [SerializeField]
    public EnemyType EnemyType;

    [SerializeField]
    public int MaxExperience;

    [SerializeField]
    public int MinExperience;

    [SerializeField]
    public int MaxMoney;

    [SerializeField]
    public int MinMoney;
}