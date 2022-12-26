using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Active»")]
public sealed class Component_Active : MonoBehaviour, IComponent_Active
{
    public event Action<bool> OnActive
    {
        add { this.controller.OnActive += value; }
        remove { this.controller.OnActive -= value; }
    }

    public event Action OnActivate
    {
        add { this.controller.OnActivate += value; }
        remove { this.controller.OnActivate -= value; }
    }

    public event Action OnDeactivate
    {
        add { this.controller.OnDeactivate += value; }
        remove { this.controller.OnDeactivate -= value; }
    }

    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public bool IsActive
    {
        get { return this.CheckIsActive(); }
    }

    [Space]
    [SerializeField]
    private ActivationBehaviour controller;

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void Activate()
    {
        this.controller.Activate();
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void Deactivate()
    {
        this.controller.Deactivate();
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            this.controller.SetActive();
        }
        else
        {
            this.controller.SetInactive();
        }
    }

    private bool CheckIsActive()
    {
        if (this.controller != null)
        {
            return this.controller.IsActive;
        }

        return default;
    }
}