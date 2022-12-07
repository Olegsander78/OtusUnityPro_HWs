using UnityEngine;

public class MoneyRepository:DataRepository<int>
{
    protected override string Key => "MoneyData";

    //private const string KEY = "MoneyData";

    public bool LoadMoney(out int money)
    {
        var result  = LoadData(out money);
        if (result)
            Debug.Log($"<color=orange>LOAD Money DATA {money}</color>");
        return result;
        

        //if (PlayerPrefs.HasKey(KEY))
        //{
        //    money = PlayerPrefs.GetInt(KEY);
        //    Debug.Log($"<color=orange>LOAD Money DATA {money}</color>");
        //    return true;
        //}

        //money = default;
        //return false;

    }

    public void SaveMoney(int money)
    {
        //PlayerPrefs.SetInt(KEY, money);
        SaveData(money);
        Debug.Log($"<color=yellow>SAVE Money DATA {money}</color>");
    }
}