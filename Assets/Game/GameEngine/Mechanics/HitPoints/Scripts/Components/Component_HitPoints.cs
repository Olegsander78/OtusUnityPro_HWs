using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Hit Points»")]
public sealed class Component_HitPoints : MonoBehaviour,
    IComponent_GetHitPoints,
    IComponent_OnHitPointsChanged
{
    public event Action<int> OnHitPointsChanged
    {
        add { this._engine.OnHitPointsChanged += value; }
        remove { this._engine.OnHitPointsChanged -= value; }
    }

    public event Action<int> OnMaxHitPointsChanged
    {
        add { this._engine.OnMaxHitPointsChanged += value; }
        remove { this._engine.OnMaxHitPointsChanged -= value; }
    }

    public int CurHitPoints
    {
        get { return this._engine.CurrentHitPoints; }
    }

    public int MaxHitPoints
    {
        get { return this._engine.MaxHitPoints; }
    }

    [SerializeField]
    private HitPointsEngine _engine;

    public void Setup(int current, int max)
    {
        this._engine.Setup(current, max);
    }

    public void SetHitPoints(int hitPoints)
    {
        this._engine.CurrentHitPoints = hitPoints;
    }

    public void SetMaxHitPoints(int hitPoints)
    {
        this._engine.MaxHitPoints = hitPoints;
    }

    public void AddHitPoints(int range)
    {
        this._engine.CurrentHitPoints += range;
    }
}
