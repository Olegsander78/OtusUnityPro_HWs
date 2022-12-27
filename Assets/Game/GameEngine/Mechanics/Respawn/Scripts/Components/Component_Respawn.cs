using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Respawn»")]
public sealed class Component_Respawn : MonoBehaviour, IComponent_Respawn
{
    [SerializeField]
    private EventBehaviour _eventReceiver;

    public void Respawn()
    {
        _eventReceiver.Call();
    }
}