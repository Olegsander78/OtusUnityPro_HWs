using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;


public static class EntitiesExtensions
{
    public static void FilterEntities(IEnumerable<IEntity> entities, IEntityCondition condition, List<IEntity> buffer)
    {
        buffer.Clear();

        foreach (var entity in entities)
        {
            if (condition.IsTrue(entity))
            {
                buffer.Add(entity);
            }
        }
    }

    public static void FilterEntities(IEnumerable<IEntity> entities, List<IEntity> buffer, Func<IEntity, bool> condition)
    {
        buffer.Clear();

        foreach (var entity in entities)
        {
            if (condition.Invoke(entity))
            {
                buffer.Add(entity);
            }
        }
    }

    public static IEntity GetClosestByDistance(Vector3 basePosition, List<IEntity> entities)
    {
        if (entities.Count <= 0)
        {
            throw new Exception("Collection is empty!");
        }

        IEntity preferedEntity = null;

        var minCost = -1.0f;

        for (int i = 0, count = entities.Count; i < count; i++)
        {
            var resource = entities[i];
            var cost = EvaluateDistance(basePosition, resource);
            if (preferedEntity == null || cost < minCost)
            {
                preferedEntity = resource;
                minCost = cost;
            }
        }

        return preferedEntity;
    }

    public static int EvaluateDistance(Vector3 basePosition, IEntity entity)
    {
        var transformComponent = entity.Get<IComponent_GetPosition>();
        var resourcePosition = transformComponent.Position;
        var vector = resourcePosition - basePosition;
        vector.y = 0;
        var distance = vector.magnitude;
        return Mathf.RoundToInt(distance);
    }
}