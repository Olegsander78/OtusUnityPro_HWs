using System;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(
    fileName = "BoosterCatalog",
    menuName = BoosterExtensions.MENU_PATH + "New BoosterCatalog"
)]
public sealed class BoosterCatalog : ScriptableObject
{
    [FormerlySerializedAs("boosterPrefabs")]
    [SerializeField]
    public BoosterConfig[] boosters;

    public BoosterConfig[] GetAllBoosters()
    {
        return this.boosters;
    }

    public BoosterConfig FindBooster(string id)
    {
        for (int i = 0, count = this.boosters.Length; i < count; i++)
        {
            var booster = this.boosters[i];
            if (booster.id == id)
            {
                return booster;
            }
        }

        throw new Exception($"Booster with id {id} is not found!");
    }
}