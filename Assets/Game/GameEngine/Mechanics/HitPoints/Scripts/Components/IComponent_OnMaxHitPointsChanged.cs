using System;

public interface IComponent_OnMaxHitPointsChanged
{
    event Action<int> OnMaxHitPointsChanged;
}