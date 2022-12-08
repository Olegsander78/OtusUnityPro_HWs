using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Id»")]
public sealed class Component_Id : MonoBehaviour, IComponent_Id
{
    public string Id
    {
        get { return this.id.Value; }
    }

    [SerializeField]
    private StringAdapter id;
}