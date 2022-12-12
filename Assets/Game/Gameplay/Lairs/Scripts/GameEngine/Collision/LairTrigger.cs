using Entities;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public sealed class LairTrigger : MonoBehaviour
{
    public IEntity Lair
    {
        get { return _lair; }
    }    

    [SerializeField]
    private UnityEntity _lair;
    
}