using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Collect» As Event")]
public sealed class Component_CollectAsEvent : MonoBehaviour, IComponent_Collect
{
    [Space]
    [SerializeField]
    private EventBehaviour collectReceiver;

    [Button]
    [GUIColor(0, 1, 0)]
    public void Collect()
    {
        this.collectReceiver.Call();
    }
}