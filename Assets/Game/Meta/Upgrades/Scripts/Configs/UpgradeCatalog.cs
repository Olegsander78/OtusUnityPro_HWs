using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "UpgradeCatalog",
    menuName = UpgradeExtensions.MENU_PATH + "New UpgradeCatalog"
)]
public sealed class UpgradeCatalog : ScriptableObject
{
    [SerializeField]
    private UpgradeConfig[] configs;

    public UpgradeConfig[] GetAllUpgrades()
    {
        return this.configs;
    }

    public UpgradeConfig FindUpgrade(string id)
    {
        var length = this.configs.Length;
        for (var i = 0; i < length; i++)
        {
            var config = this.configs[i];
            if (config.Id == id)
            {
                return config;
            }
        }

        throw new Exception($"Config with {id} is not found!");
    }
}