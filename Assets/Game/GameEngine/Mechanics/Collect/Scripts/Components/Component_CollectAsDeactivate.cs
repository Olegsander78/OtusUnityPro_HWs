using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Collect» As Deactivate")]
public sealed class Component_CollectAsDeactivate : MonoBehaviour, IComponent_Collect
{
    [Space]
    [SerializeField]
    private ActivationBehaviour collectReceiver;

    [Button]
    [GUIColor(0, 1, 0)]
    public void Collect()
    {
        this.collectReceiver.Deactivate();
    }
}