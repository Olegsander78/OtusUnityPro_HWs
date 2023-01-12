using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class UpgradeTest : MonoBehaviour, 
    IGameElementGroup
{
    [SerializeField]
    private UpgradesManager _upgradesManager;
    
    [SerializeField]
    private UpgradeConfig _targetConfig;

    private Upgrade _targetUpgrade;

    [Button]
    private void DoUpgrade()
    {
        //if (!_targetUpgrade.IsMaxUpgradeLevel)
        //{
        //    _targetUpgrade.LevelUp();
        //    _upgradesManager.LevelUp(_targetUpgrade);
        //    Debug.Log($"Level Up {_targetUpgrade.UpgradeLevel}");
        //}

        _upgradesManager.LevelUp(_targetUpgrade);
        Debug.Log($"Level Up {_targetUpgrade.UpgradeLevel}");
    }

    private void Awake()
    {
        _targetUpgrade = _targetConfig.InstantiateUpgrade();
    }

    IEnumerable<IGameElement> IGameElementGroup.GetElements()
    {
        if (_targetUpgrade is IGameElement gameUpgrade)
        {
            yield return gameUpgrade;
        }
    }
}