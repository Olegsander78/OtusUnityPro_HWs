using System.Collections.Generic;
using Entities;
using UnityEngine;


public abstract class EntitiesProvider : MonoBehaviour
{
    public abstract IEnumerable<IEntity> ProvideEntities();
}