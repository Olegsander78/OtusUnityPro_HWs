using System;


public interface IComponent_Enable
{
    event Action<bool> OnEnabled;

    bool IsEnable { get; }

    void SetEnable(bool isEnable);
}