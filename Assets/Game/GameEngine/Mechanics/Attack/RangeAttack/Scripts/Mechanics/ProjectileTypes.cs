using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;

public class ProjectileTypes : MonoBehaviour
{
    public enum Projectiles
    {
        Fireball = 1,
        Thunderball = 2,
        Bullet = 3
    }

    //[System.Serializable]
    //public class ProjectilesNote
    //{
    //    public Projectiles Projectiles;
    //    public GameObject PrefabProjectile;
    //}

    //[SerializeField]
    //private List<ProjectilesNote> _projectilesNotes;

    [SerializeField]
    private Dictionary<Projectiles, GameObject> _projectilesDictionary;
}
