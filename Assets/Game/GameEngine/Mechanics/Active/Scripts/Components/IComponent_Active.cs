using System;


public interface IComponent_Active
{
    event Action<bool> OnActive;

    event Action OnActivate;

    event Action OnDeactivate;

    bool IsActive { get; }

    void SetActive(bool isActive);

    void Activate();

    void Deactivate();
}