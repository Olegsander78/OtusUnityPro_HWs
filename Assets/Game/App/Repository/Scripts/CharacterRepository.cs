using UnityEngine;

public sealed class CharacterRepository
{
    private const string PLAYER_PREFS_KEY = "CharacterData";

    public bool LoadStats(out CharacterData data)
    {
        if (PlayerPrefs.HasKey(PLAYER_PREFS_KEY))
        {
            var json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);
            data = JsonUtility.FromJson<CharacterData>(json);

            Debug.Log($"<color=orange>LOAD CHARACTER DATA {json}</color>");
            return true;
        }

        data = default;
        return false;
    }

    public void SaveStats(CharacterData data)
    {
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);

        Debug.Log($"<color=yellow>SAVE CHARACTER DATA {json}</color>");
    }
}