using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;


public sealed class HitPointsEngine : MonoBehaviour
{
    public event Action OnSetuped;

    public event Action<int> OnLevelChanged;

    public event Action<int> OnMaxHitPointsChanged;

    public event Action OnHitPointsFull;

    public event Action OnHitPointsEmpty;

    public int CurrentHitPoints
    {
        get { return _currentHitPoints.Value; }
        set { SetHitPoints(value); }
    }

    public int MaxHitPoints
    {
        get { return _maxHitPoints.Value; }
        set { SetMaxHitPoints(value); }
    }

    [SerializeField]
    private IntBehaviour _maxHitPoints;

    [SerializeField]
    private IntBehaviour _currentHitPoints;

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Setup(int current, int max)
    {
        _maxHitPoints.Value = max;
        _currentHitPoints.Value = Mathf.Clamp(current, 0, _maxHitPoints.Value);
        OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetHitPoints(int value)
    {
        value = Mathf.Clamp(value, 0, _maxHitPoints.Value);
        _currentHitPoints.Value = value;
        OnLevelChanged?.Invoke(_currentHitPoints.Value);

        if (value <= 0)
        {
            OnHitPointsEmpty?.Invoke();
        }

        if (value >= _maxHitPoints.Value)
        {
            OnHitPointsFull?.Invoke();
        }
    }

    [Button]
    [GUIColor(0, 1, 0)]
    private void SetMaxHitPoints(int value)
    {
        value = Math.Max(1, value);
        if (_currentHitPoints.Value > value)
        {
            _currentHitPoints.Value = value;
        }

        _maxHitPoints.Value = value;
        OnMaxHitPointsChanged?.Invoke(value);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _maxHitPoints.Value = Math.Max(1, _maxHitPoints.Value);
        _currentHitPoints.Value = Mathf.Clamp(_currentHitPoints.Value, 1, _maxHitPoints.Value);
    }
#endif
}
