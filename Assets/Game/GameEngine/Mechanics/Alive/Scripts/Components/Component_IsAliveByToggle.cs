using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Is Alive» By Toggle")]
public sealed class Component_IsAliveByToggle : MonoBehaviour, IComponent_IsAlive
{
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public bool IsAlive
    {
        get { return this.CheckIsAlive(); }
    }

    [SerializeField]
    private bool invert;

    [Space]
    [SerializeField]
    private BoolBehaviour isAlive;

    private bool CheckIsAlive()
    {
        if (this.isAlive == null)
        {
            return default;
        }

        if (this.invert)
        {
            return !this.isAlive.Value;
        }

        return this.isAlive.Value;
    }
}