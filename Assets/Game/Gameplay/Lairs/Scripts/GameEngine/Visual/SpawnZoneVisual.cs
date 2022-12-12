using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class SpawnZoneVisual : MonoBehaviour
{
    public event Action OnKilledEnemy;

    public event Action OnRevivedEnemy;
    
    [SerializeField]
    private List<GameObject> _enemeis;

    private int _currentAmountEnemies;

    public void SetupEnemies(int currentAmount)
    {
        _currentAmountEnemies = Mathf.Min(currentAmount, _enemeis.Count);

        for (var i = 0; i < currentAmount; i++)
        {
            var item = _enemeis[i];
            item.SetActive(true);
        }

        var count = _enemeis.Count;
        for (var i = currentAmount; i < count; i++)
        {
            var item = _enemeis[i];
            item.SetActive(false);
        }
    }

    [Button]
    public void KillEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        OnKilledEnemy?.Invoke();
    }

    [Button]
    public void RiviveEnemy()
    {
        GameObject enemy = _enemeis.Find(x => x.activeInHierarchy == false);

        enemy.SetActive(true);

        OnRevivedEnemy?.Invoke();
    }

    public void IncrementEnemies(int range)
    {
        var previousAmount = _currentAmountEnemies;
        var newAmount = Mathf.Min(_currentAmountEnemies + range, _enemeis.Count);
        _currentAmountEnemies = newAmount;

        for (var i = previousAmount; i < newAmount; i++)
        {
            var item = _enemeis[i];
            item.SetActive(true);
        }
    }

    public void DecrementEnemeis(int range)
    {
        var previousAmount = _currentAmountEnemies;
        var newAmount = Mathf.Max(_currentAmountEnemies - range, 0);
        _currentAmountEnemies = newAmount;

        for (var i = previousAmount - 1; i >= newAmount; i--)
        {
            var item = _enemeis[i];
            item.SetActive(false);
        }
    }

#if UNITY_EDITOR
    [Button("Setup Items")]
    private void Editor_SetupItems()
    {
        this._enemeis = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            this._enemeis.Add(child.gameObject);
        }
    }
#endif
}