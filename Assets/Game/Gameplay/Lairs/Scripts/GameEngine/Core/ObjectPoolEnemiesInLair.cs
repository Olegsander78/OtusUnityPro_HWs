using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolEnemiesInLair : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private SpawnEnemyInLairEngine _enemySpawnEngine;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _enemySpawnEngine.MaxEnemies; i++)
        {
            CreateNewObject();
        }
    }
    GameObject CreateNewObject()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.SetActive(false);
        pooledEnemies.Add(enemy);

        return enemy;
    }

    public GameObject GetObject()
    {
        GameObject enemy = pooledEnemies.Find(x => x.activeInHierarchy == false);

        if (enemy == null)
        {
            enemy = CreateNewObject();
        }

        enemy.SetActive(true);

        return enemy;
    }
}
