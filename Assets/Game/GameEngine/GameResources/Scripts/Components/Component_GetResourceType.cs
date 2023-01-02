using UnityEngine;


[AddComponentMenu("GameEngine/GameResources/Component «Get Resource Type»")]
public sealed class Component_GetResourceType : MonoBehaviour, IComponent_GetResourceType
{
    public ResourceType ResourceType
    {
        get { return this.type; }
    }

    [SerializeField]
    private ResourceType type;
}