using UnityEngine;
using Elementary;
using System;
using Sirenix.OdinInspector;
using Entities;

public class ExperienceEngine : MonoBehaviour
{
    public event Action OnSetuped;

    public event Action<int> OnExpChanged;

    public event Action<int> OnExpChangedOnClick;

    public event Action<int> OnExpSpendedOnClick;

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
        //_expReceiver.OnEvent += OnAddedExpWithAutoLevelUp;
        _expReceiver.OnEvent += OnAddedExpWithoutAutoSpending;
        _expReceiver.OnEvent += OnSpendedExpForLevelUpOnClick;
    }

    private void OnDisable()
    {
        //_expReceiver.OnEvent -= OnAddedExpWithAutoLevelUp;
        _expReceiver.OnEvent -= OnAddedExpWithoutAutoSpending;
        _expReceiver.OnEvent -= OnSpendedExpForLevelUpOnClick;
    }

    //[Title("Methods")]
    //[GUIColor(0, 1, 0)]
    //[Button]
    public void Setup(int curExp, int nextLvlExp, int totalExp)
    {
        _currentExp.Value = curExp;
        _nextLevelExp.Value = nextLvlExp;
        _totalExp.Value = totalExp;
        OnSetuped?.Invoke();
    }

    //[GUIColor(0, 1, 0)]
    //[Button]
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

    //[GUIColor(0, 1, 0)]
    //[Button]
    private void SetNextLevelExp()
    {
        _nextLevelExp.Value = 100 * _levelUpEngine.CurrentLevel;
        OnNextLvlExpChanged?.Invoke(_nextLevelExp.Value);
    }

    //[GUIColor(0, 1, 0)]
    //[Button]
    private void SetTotalExp(int value)
    {
        _totalExp.Value += value;
    }


    // Experience auto-calculate levels
    //[GUIColor(0, 1, 0)]
    //[Button]
    private void OnAddedExpWithAutoLevelUp(int value)
    {
        _totalExp.Value += value;
        _currentExp.Value += value;

        while(_currentExp.Value >= _nextLevelExp.Value)
        {
            _currentExp.Value = _currentExp.Value - _nextLevelExp.Value;
            _levelUpEngine.CurrentLevel++;
            SetNextLevelExp();
        }
        
        OnExpChanged?.Invoke(_currentExp.Value);
    }

    // Experience calculate for OnClick
    [GUIColor(0, 1, 0)]
    [Button]
    public void OnAddedExpWithoutAutoSpending(int value)
    {
        _totalExp.Value += value;
        _currentExp.Value += value;

        OnExpChangedOnClick?.Invoke(_currentExp.Value);
    }
    // Experience calculate levels OnClick
    //[GUIColor(0, 1, 0)]
    //[Button]
    public void OnSpendedExpForLevelUpOnClick(int value)
    {        
        if (value >= _nextLevelExp.Value)
        {
            _currentExp.Value = Mathf.Max(0, _currentExp.Value - _nextLevelExp.Value);
            value = value - _nextLevelExp.Value;
            _levelUpEngine.CurrentLevel++;
            SetNextLevelExp();
        }

        OnExpSpendedOnClick?.Invoke(_currentExp.Value);
    }
}
