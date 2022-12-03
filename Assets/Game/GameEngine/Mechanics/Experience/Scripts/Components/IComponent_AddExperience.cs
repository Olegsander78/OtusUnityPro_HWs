using System;

public interface IComponent_AddExperience
{
    event Action<int> OnExperienceChanged;

    event Action<int> OnNextlvlExperienceChanged;

    event Action<int> OnTotalExperienceChanged;
    void AddExperience(int experience);
}
