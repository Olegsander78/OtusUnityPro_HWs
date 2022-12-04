using System;

public interface IComponent_SpendExperience
{
    event Action<int> OnSpendExperience;
    void SpendExpPerLevel(int experience);
}
