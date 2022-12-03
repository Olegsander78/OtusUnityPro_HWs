using UnityEngine;
using Elementary;
using System;
using Sirenix.OdinInspector;
using Entities;

public class ExperienceMechanics : MonoBehaviour
{
    public event Action OnSetuped;

    public event Action<int> OnExpChanged;

    public event Action<int> OnNextLvlExpChanged;
        
    public event Action OnExpFull;

    [SerializeField]
    private EventReceiver_Int _expReceiver;

    [SerializeField]
    private IntBehaviour _currentExp;

    [SerializeField]
    private IntBehaviour _nextLevelExp;

    [SerializeField]
    private IntBehaviour _totalExp;

    [SerializeField]
    private LevelUpEngine _levelUpEngine;

    [SerializeField]
    private UnityEntity _character;


    public int CurrentExp
    {
        get { return _currentExp.Value; }
        set { SetExp(value); }
    }    

    public int NextLevelExp
    {
        get { return _nextLevelExp.Value; }
        set { SetNextLevelExp(); }
    }  

    public int TotalExp
    {
        get { return _totalExp.Value; }
        set { SetTotalExp(value); }
    }
    private void OnEnable()
    {
        _expReceiver.OnEvent += OnAddedExp;
    }

    private void OnDisable()
    {
        _expReceiver.OnEvent -= OnAddedExp;
    }

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    private void Setup(int curExp, int nextLvlExp, int totalExp)
    {
        _currentExp.Value = curExp;
        _nextLevelExp.Value = nextLvlExp;
        _totalExp.Value = totalExp;
        OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetExp(int value)
    {        
        _currentExp.Value = value;
        _totalExp.Value += value;
        OnExpChanged?.Invoke(_currentExp.Value);

        if (value >= _nextLevelExp.Value)
        {
            OnExpFull?.Invoke();
        }
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetNextLevelExp()
    {
        _nextLevelExp.Value = 100 * _character.Get<IComponent_GetLevel>().Level;
        OnNextLvlExpChanged?.Invoke(_nextLevelExp.Value);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetTotalExp(int value)
    {
        _totalExp.Value += value;
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void OnAddedExp(int value)
    {
        _totalExp.Value += value;
        _currentExp.Value += value;

        while(_currentExp.Value >= _nextLevelExp.Value)
        {
            _currentExp.Value = _currentExp.Value - _nextLevelExp.Value;
            _character.Get<IComponent_AddLevel>().AddLevel(1);
            SetNextLevelExp();
        }        
    }
}
