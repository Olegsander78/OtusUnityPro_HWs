using System;

public interface IComponent_OnLevelChanged
{
    event Action<int> OnLevelChanged;

    event Action<int> OnMaxLevelChanged;
}
