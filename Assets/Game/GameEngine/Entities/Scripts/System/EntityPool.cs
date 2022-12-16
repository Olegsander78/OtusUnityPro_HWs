using System.Collections.Generic;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class EntityPool : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    [SerializeField]
    private UnityEntity prefab;

    [ReadOnly]
    [ShowInInspector]
    private readonly Queue<UnityEntity> availableEntities;

    public EntityPool()
    {
        this.availableEntities = new Queue<UnityEntity>();
    }

    public UnityEntity Get()
    {
        UnityEntity entity;
        if (this.availableEntities.Count > 0)
        {
            entity = this.availableEntities.Dequeue();
            entity.gameObject.hideFlags = HideFlags.None;
        }
        else
        {
            entity = Instantiate(this.prefab, this.parent);
        }

        return entity;
    }

    public void Release(UnityEntity entity)
    {
        var entityObject = entity.gameObject;
        entityObject.SetActive(false);
        entityObject.hideFlags = HideFlags.HideInHierarchy;
        this.availableEntities.Enqueue(entity);
    }
}