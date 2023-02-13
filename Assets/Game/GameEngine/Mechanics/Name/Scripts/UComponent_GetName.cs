using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Name/Component «Get Name»")]
public sealed class UComponent_GetName : MonoBehaviour, IComponent_GetName
{
    public string Name
    {
        get { return _name.Value; }
    }

    [SerializeField]
    private StringAdapter _name;
}