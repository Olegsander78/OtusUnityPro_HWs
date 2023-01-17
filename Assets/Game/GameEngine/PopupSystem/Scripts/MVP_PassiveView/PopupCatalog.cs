using System;
using UIFrames.Unity;
using UnityEngine;


[CreateAssetMenu(
    fileName = "PopupCatalog",
    menuName = "GameEngine/GUI/New PopupCatalog"
)]
public sealed class PopupCatalog : ScriptableObject
{
    [Space]
    [SerializeField]
    private PopupInfo[] popups = Array.Empty<PopupInfo>();

    public UnityFrame LoadPrefab(PopupName name)
    {
        for (int i = 0, count = this.popups.Length; i < count; i++)
        {
            var info = this.popups[i];
            if (info.name == name)
            {
                return info.prefab;
            }
        }

        throw new Exception($"Prefab {name} is not found!");
    }

    [Serializable]
    private sealed class PopupInfo
    {
        [SerializeField]
        public PopupName name;

        [SerializeField]
        public UnityFrame prefab;
    }
}