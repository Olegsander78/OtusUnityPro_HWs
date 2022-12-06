using UnityEngine;

public sealed class MoneyRepository
{
    private const string KEY = "Money";

    public bool LoadMoney(out int money)
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            money = PlayerPrefs.GetInt(KEY);
            Debug.Log($"<color=orange>LOAD Money DATA {money}</color>");
            return true;
        }

        money = default;
        return false;
    }

    public void SaveMoney(int money)
    {
        PlayerPrefs.SetInt(KEY, money);
        Debug.Log($"<color=yellow>SAVE Money DATA {money}</color>");
    }
}