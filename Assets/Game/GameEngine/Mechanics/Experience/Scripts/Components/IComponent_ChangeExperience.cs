using System;

public interface IComponent_ChangeExperience
{
    event Action<int> OnExperienceChanged;

    event Action<int> OnNextlvlExperienceChanged;

    event Action<int> OnTotalExperienceChanged;
    void ChangeExperience(int experience);
}
