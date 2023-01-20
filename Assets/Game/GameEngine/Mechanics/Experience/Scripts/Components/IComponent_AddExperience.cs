using System;

public interface IComponent_AddExperience
{
    event Action<int> OnAddExperience;
    void AddExperience(int experience);
}