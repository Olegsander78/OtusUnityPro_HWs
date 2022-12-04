using Elementary;
using System;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Experience»")]
public class Component_Experience :MonoBehaviour, 
    IComponent_ChangeExperience,
    IComponent_GetExperience,
    IComponent_SpendExperience
{
    public event Action<int> OnExperienceChanged
    {
        add { _currentExperience.OnValueChanged += value; }
        remove { _currentExperience.OnValueChanged -= value; }
    }

    public event Action<int> OnNextlvlExperienceChanged
    {
        add { _toNextLevelExperience.OnValueChanged += value; }
        remove { _toNextLevelExperience.OnValueChanged -= value; }
    }

    public event Action<int> OnTotalExperienceChanged
    {
        add { _toNextLevelExperience.OnValueChanged+=value; }
        remove { _toNextLevelExperience.OnValueChanged-=value; }
    }

    public event Action<int> OnSpendExperience;

    [SerializeField]
    private ExperienceEngine _experienceEngine;
    
    [SerializeField]
    private EventReceiver_Int _changeExpReceiver;      

    public int CurrentExperience
    {
        get { return _experienceEngine.CurrentExp; }
    }
    public int ToNextLevelExperience
    {
        get { return _experienceEngine.NextLevelExp; }
    }
    public int TotalExperience
    {
        get { return _experienceEngine.TotalExp; }
    }

    [SerializeField]
    private IntBehaviour _currentExperience;    

    [SerializeField]
    private IntBehaviour _toNextLevelExperience;    

    [SerializeField]
    private IntBehaviour _totalExperience;

    public void ChangeExperience(int experience)
    {
        _changeExpReceiver.Call(experience);
    }
    
    public void SpendExpPerLevel(int exp)
    {
        _experienceEngine.OnSpendedExpForLevelUpOnClick(exp);

        OnSpendExperience?.Invoke(exp);
    }
}
