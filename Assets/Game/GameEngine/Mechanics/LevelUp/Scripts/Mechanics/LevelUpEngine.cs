using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;

public sealed class LevelUpEngine : MonoBehaviour
{
    public event Action OnSetuped;

    public event Action<int> OnLevelChanged;

    public event Action<int> OnMaxLevelChanged;

    public event Action OnLevelUpFull;

    public int CurrentLevel
    {
        get { return _currentLevel.Value; }
        set { SetLevel(value); }
    }

    public int MaxLevel
    {
        get { return _maxLevel.Value; }
        set { SetMaxLevel(value); }
    }
    
    [SerializeField]
    private IntBehaviour _currentLevel;

    [SerializeField]
    private IntBehaviour _maxLevel;    

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Setup(int current, int max)
    {
        _maxLevel.Value = max;
        _currentLevel.Value = Mathf.Clamp(current, 1, _maxLevel.Value);
        OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetLevel(int value)
    {
        value = Mathf.Clamp(value, 1, _maxLevel.Value);
        _currentLevel.Value = value;
        OnLevelChanged?.Invoke(_currentLevel.Value);

        if (value >= _maxLevel.Value)
        {
            OnLevelUpFull?.Invoke();
        }
    }

    [Button]
    [GUIColor(0, 1, 0)]
    private void SetMaxLevel(int value)
    {
        value = Math.Max(1, value);
        if (_maxLevel.Value > value)
        {
            _maxLevel.Value = value;
        }

        _maxLevel.Value = value;
        OnMaxLevelChanged?.Invoke(value);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _maxLevel.Value = Math.Max(1, _maxLevel.Value);
        _currentLevel.Value = Mathf.Clamp(_currentLevel.Value, 1, _maxLevel.Value);
    }
#endif
}
