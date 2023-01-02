using UnityEngine;


public sealed class Component_ResourceObject : MonoBehaviour,
    IComponent_GetResourceType,
    IComponent_GetResourceCount
{    
    public ResourceType ResourceType
    {
        get { return _info.type; }
    }    

    public int ResourceCount
    {
        get { return Random.Range(_info.minCount, _info.count + 1); }
    }

    [SerializeField]
    private ScriptableResourceObject _info;
}