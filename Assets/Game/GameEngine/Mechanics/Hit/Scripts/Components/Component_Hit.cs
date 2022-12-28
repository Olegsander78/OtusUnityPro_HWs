using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Hit»")]
public sealed class Component_Hit : MonoBehaviour, IComponent_Hit
{
    [SerializeField]
    private EventBehaviour receiver;

    public void Hit()
    {
        this.receiver.Call();
    }
}