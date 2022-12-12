using Entities;
using Sirenix.OdinInspector;
using UnityEngine;

public class ConveyorLairTest : MonoBehaviour
{
    [SerializeField]
    private UnityEntity _lair;

    [SerializeField]
    private SpawnZoneVisual _spawnZoneVisual;

    [SerializeField]
    private GameObject _enemyPrefab;

    [Button]
    private void SpawnEnemy()
    {
        _lair.Get<IComponent_UnloadZone>().SetupAmount(_lair.Get<IComponent_UnloadZone>().CurrentAmount + 1);
    }

    [Button]
    private void KillEnemy()
    {
        _spawnZoneVisual.KillEnemy(_enemyPrefab);
    }
}
