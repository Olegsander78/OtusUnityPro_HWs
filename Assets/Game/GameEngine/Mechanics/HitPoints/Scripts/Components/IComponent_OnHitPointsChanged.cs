using System;

public interface IComponent_OnHitPointsChanged
{
    event Action<int> OnHitPointsChanged;

    event Action<int> OnMaxHitPointsChanged;
}
