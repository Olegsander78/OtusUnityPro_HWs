using UnityEngine;

public class MoneyRepository:DataRepository<int>
{
    protected override string Key => "MoneyData";

    public bool LoadMoney(out int money)
    {
        var result  = LoadData(out money);
        if (result)
            Debug.Log($"<color=orange>LOAD Money DATA: {money}</color>");
        return result;
    }

    public void SaveMoney(int money)
    {
        SaveData(money);
        Debug.Log($"<color=yellow>SAVE Money DATA: {money}</color>");
    }
}