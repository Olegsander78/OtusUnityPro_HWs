using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class LairsService : MonoBehaviour
{
    [PropertySpace]
    [ReadOnly]
    [ShowInInspector]
    private IEntity[] _lairs;

    public IEntity FindLair(string id)
    {
        for (int i = 0, count = _lairs.Length; i < count; i++)
        {
            var lair = _lairs[i];
            var lairId = lair.Get<IComponent_Id>().Id;
            if (lairId == id)
            {
                return lair;
            }
        }

        throw new Exception($"Lair with {id} is not found!");
    }

    public IEntity[] GetAllLairs()
    {
        return _lairs;
    }

    public void SetupLairs(IEnumerable<IEntity> lairs)
    {
        _lairs = lairs.ToArray();
    }
}