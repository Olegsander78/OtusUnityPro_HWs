using System;
using System.Collections.Generic;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class EntitiesService : MonoBehaviour
{
    public event Action<IEntity> OnAdded;

    public event Action<IEntity> OnRemoved;

    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public int EntityCount
    {
        get { return this.entities.Count; }
    }

    [Space]
    [ReadOnly]
    [ShowInInspector]
    private readonly HashSet<IEntity> entities;

    public EntitiesService()
    {
        this.entities = new HashSet<IEntity>();
    }

    public void Setup(IEnumerable<IEntity> entities)
    {
        this.entities.Clear();
        this.entities.UnionWith(entities);
    }

    public void AddEntity(IEntity entity)
    {
        if (this.entities.Add(entity))
        {
            this.OnAdded?.Invoke(entity);
        }
    }

    public void AddEntities(IEnumerable<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            this.AddEntity(entity);
        }
    }

    public void RemoveEntity(IEntity entity)
    {
        if (this.entities.Remove(entity))
        {
            this.OnRemoved?.Invoke(entity);
        }
    }

    public void RemoveEntitites(IEnumerable<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            this.RemoveEntity(entity);
        }
    }

    public bool FindEntity(out IEntity result, Func<IEntity, bool> predicate)
    {
        foreach (var entity in this.entities)
        {
            if (predicate.Invoke(entity))
            {
                result = entity;
                return true;
            }
        }

        result = null;
        return false;
    }

    public bool FindEntity(IEntityCondition condition, out IEntity result)
    {
        foreach (var entity in this.entities)
        {
            if (condition.IsTrue(entity))
            {
                result = entity;
                return true;
            }
        }

        result = default;
        return false;
    }


    public bool FindEntityWithElement<T>(out IEntity result)
    {
        foreach (var entity in this.entities)
        {
            if (entity.TryGet(out T _))
            {
                result = entity;
                return true;
            }
        }

        result = default;
        return false;
    }

    public bool FindEntityWithElement<T>(out IEntity result, Func<T, bool> predicate)
    {
        foreach (var entity in this.entities)
        {
            if (entity.TryGet(out T component) && predicate.Invoke(component))
            {
                result = entity;
                return true;
            }
        }

        result = default;
        return false;
    }

    public IEnumerable<IEntity> GetAllEntities()
    {
        return this.entities;
    }

    public IEnumerable<IEntity> FindEntities(IEntityCondition condition)
    {
        foreach (var entity in this.entities)
        {
            if (condition.IsTrue(entity))
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<IEntity> FindEntities(Func<IEntity, bool> predicate)
    {
        foreach (var entity in this.entities)
        {
            if (predicate.Invoke(entity))
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<IEntity> FindEntitiesWithElement<T>()
    {
        foreach (var entity in this.entities)
        {
            if (entity.TryGet(out T _))
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<IEntity> FindEntitiesWithElement<T>(Func<T, bool> predicate)
    {
        foreach (var entity in this.entities)
        {
            if (entity.TryGet(out T component) && predicate.Invoke(component))
            {
                yield return entity;
            }
        }
    }
}