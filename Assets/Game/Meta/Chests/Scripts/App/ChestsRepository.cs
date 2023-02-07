using Game.App;
using UnityEngine;

public sealed class ChestsRepository : DataArrayRepository<ChestData>
{
    protected override string Key => "Chests";

    public bool LoadChests(out ChestData[] chestsData)
    {
        //return LoadData(out chestsData);
        var result = LoadData(out chestsData);
        if (result)
        {
            foreach (var item in chestsData)
            {                
                var id = item.id;
                var time = item.remainingTime;
                Debug.Log($"<color=red>LOAD Chests DATA: {id}:{time}</color>");
            }
        }
            
        return result;
    }

    public void SaveChests(ChestData[] chestsData)
    {
        SaveData(chestsData);
        foreach (var item in chestsData)
        {
            var id = item.id;
            var time = item.remainingTime;
            Debug.Log($"<color=green>SAVE Chests DATA: {id}:{time}</color>");
        }
        //Debug.Log($"<color=blue>SAVE Chest DATA: {chestsData}</color>");
    }
}