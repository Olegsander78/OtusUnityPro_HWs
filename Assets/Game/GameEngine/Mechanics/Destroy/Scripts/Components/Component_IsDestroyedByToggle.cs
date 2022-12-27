using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Is Destroyed» By Toggle")]
public sealed class Component_IsDestroyedByToggle : MonoBehaviour, IComponent_IsDestroyed
{
    public bool IsDestroyed
    {
        get { return this.CheckDestroyed(); }
    }

    [SerializeField]
    private bool invert;

    [SerializeField]
    private BoolBehaviour isDestroyed;

    private bool CheckDestroyed()
    {
        if (this.invert)
        {
            return this.isDestroyed.Value;
        }

        return this.isDestroyed.Value;
    }
}