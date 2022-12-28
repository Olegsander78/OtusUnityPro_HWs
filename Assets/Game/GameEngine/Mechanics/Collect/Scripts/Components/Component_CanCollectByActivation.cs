using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Can Collect» By Activation")]
public sealed class Component_CanCollectByActivation : MonoBehaviour, IComponent_CanCollect
{
    public bool CanCollect
    {
        get { return this.activationController.IsActive; }
    }

    [SerializeField]
    private ActivationBehaviour activationController;
}