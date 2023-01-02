using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/GameResources/Component «Get Resource Count»")]
public sealed class Component_GetResourceCount : MonoBehaviour, IComponent_GetResourceCount
{
    public int ResourceCount
    {
        get { return this.adapter.Value; }
    }

    [SerializeField]
    private IntAdapter adapter;
}