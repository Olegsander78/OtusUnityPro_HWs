using System;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(
    fileName = "ChestsCatalog",
    menuName = "Meta/Chests/New Chests Catalog"
)]
public sealed class ChestCatalog : ScriptableObject
{
    [FormerlySerializedAs("ChestsPrefabs")]
    [SerializeField]
    public ChestConfig[] Chests;

    public ChestConfig[] GetAllChests()
    {
        return Chests;
    }

    public ChestConfig FindChest(string id)
    {
        for (int i = 0, count = Chests.Length; i < count; i++)
        {
            var chest = Chests[i];
            if (chest.Id == id)
            {
                return chest;
            }
        }

        throw new Exception($"Chest with id {id} is not found!");
    }
}