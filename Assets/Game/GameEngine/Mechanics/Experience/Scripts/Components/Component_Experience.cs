using Elementary;
using System;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Experience»")]
public class Component_Experience :MonoBehaviour, 
    IComponent_ChangeExperience,
    IComponent_GetExperience
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

    [SerializeField]
    private EventReceiver_Int _changeExpReceiver;      

    public int CurrentExperience
    {
        get { return _currentExperience.Value; }
    }
    public int ToNextLevelExperience
    {
        get { return _toNextLevelExperience.Value; }
    }
    public int TotalExperience
    {
        get { return _totalExperience.Value; }
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
}
