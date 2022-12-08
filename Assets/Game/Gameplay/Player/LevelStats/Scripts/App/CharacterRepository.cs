using UnityEngine;

public sealed class CharacterRepository: DataRepository<CharacterData>
{
    protected override string Key => "CharacterData";

    public bool LoadCharacter(out CharacterData characterData)
    {
        var result = LoadData(out characterData);
        if (result)
            Debug.Log($"<color=orange>LOAD CHARACTER DATA: {characterData}</color>");
        return result;
    }

    public void SaveCharacter(CharacterData characterData)
    {
        SaveData(characterData);
        Debug.Log($"<color=yellow>SAVE CHARACTER DATA: {characterData}</color>");
    }
}