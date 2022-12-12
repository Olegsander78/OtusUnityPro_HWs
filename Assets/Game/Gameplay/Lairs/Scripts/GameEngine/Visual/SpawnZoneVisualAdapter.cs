using Elementary;
using System;
using UnityEngine;


public sealed class SpawnZoneVisualAdapter : MonoBehaviour
{
    [SerializeField]
    private LimitedIntBehavior _spawner;

    [SerializeField]
    private SpawnZoneVisual _spawnVisualZone;

    private void Awake()
    {
        _spawnVisualZone.SetupEnemies(_spawner.Value);
    }

    private void OnEnable()
    {
        _spawner.OnValueChanged += OnEnemiesChanged;
        _spawnVisualZone.OnKilledEnemy += OnEnemiesKilled;
        _spawnVisualZone.OnRevivedEnemy += OnEnemiesRevived;
    }    

    private void OnDisable()
    {
        _spawner.OnValueChanged -= OnEnemiesChanged;
        _spawnVisualZone.OnKilledEnemy -= OnEnemiesKilled;
        _spawnVisualZone.OnRevivedEnemy -= OnEnemiesRevived;
    }

    private void OnEnemiesChanged(int count)
    {
        _spawnVisualZone.SetupEnemies(count);
    }
    private void OnEnemiesRevived()
    {
        if (!_spawner.IsLimit)
            _spawner.Value++;            
    }

    private void OnEnemiesKilled()
    {
        if (_spawner.Value != 0)
            _spawner.Value--;
    }
}