using System;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class HitPointsEngine : MonoBehaviour
{
    public event Action OnSetuped;

    public event Action<int> OnHitPointsChanged;

    public event Action<int> OnMaxHitPointsChanged;

    public event Action OnHitPointsFull;

    public event Action OnHitPointsEmpty;

    public int CurrentHitPoints
    {
        get { return this.currentHitPoints; }
        set { this.SetHitPoints(value); }
    }

    public int MaxHitPoints
    {
        get { return this.maxHitPoints; }
        set { this.SetMaxHitPoints(value); }
    }

    [SerializeField]
    private int maxHitPoints;

    [SerializeField]
    private int currentHitPoints;

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Setup(int current, int max)
    {
        this.maxHitPoints = max;
        this.currentHitPoints = Mathf.Clamp(current, 0, this.maxHitPoints);
        this.OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void SetHitPoints(int value)
    {
        value = Mathf.Clamp(value, 0, this.maxHitPoints);
        this.currentHitPoints = value;
        this.OnHitPointsChanged?.Invoke(this.currentHitPoints);

        if (value <= 0)
        {
            this.OnHitPointsEmpty?.Invoke();
        }

        if (value >= this.maxHitPoints)
        {
            this.OnHitPointsFull?.Invoke();
        }
    }

    [Button]
    [GUIColor(0, 1, 0)]
    private void SetMaxHitPoints(int value)
    {
        value = Math.Max(1, value);
        if (this.currentHitPoints > value)
        {
            this.currentHitPoints = value;
        }

        this.maxHitPoints = value;
        this.OnMaxHitPointsChanged?.Invoke(value);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        this.maxHitPoints = Math.Max(1, this.maxHitPoints);
        this.currentHitPoints = Mathf.Clamp(this.currentHitPoints, 1, this.maxHitPoints);
    }
#endif
}
