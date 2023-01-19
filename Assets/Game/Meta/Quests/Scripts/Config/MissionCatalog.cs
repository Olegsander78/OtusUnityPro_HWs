using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "MissionCatalog",
    menuName = MissionExtensions.MENU_PATH + "New MissionCatalog"
)]
public sealed class MissionCatalog : ScriptableObject
{
    [SerializeField]
    public MissionConfig[] missions;

    public MissionConfig[] GetAllMissions()
    {
        return this.missions;
    }

    public MissionConfig FindMission(string id)
    {
        for (int i = 0, count = this.missions.Length; i < count; i++)
        {
            var mission = this.missions[i];
            if (mission.Id == id)
            {
                return mission;
            }
        }

        throw new Exception($"Mission with id {id} is not found!");
    }

    public MissionConfig[] FindMissions(MissionDifficulty difficulty)
    {
        var missions = new List<MissionConfig>();
        for (int i = 0, count = this.missions.Length; i < count; i++)
        {
            var mission = this.missions[i];
            if (mission.Difficulty == difficulty)
            {
                missions.Add(mission);
            }
        }

        return missions.ToArray();
    }

    public MissionConfig[] FindMissions(Predicate<MissionConfig> predicate)
    {
        return Array.FindAll(this.missions, predicate);
    }
}