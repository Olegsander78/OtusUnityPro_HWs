using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «LevelUp»")]
public sealed class Component_LevelUp : MonoBehaviour,
    IComponent_GetLevel,
    IComponent_OnLevelChanged
{
    public event Action<int> OnLevelChanged
    {
        add { _engine.OnLevelChanged += value; }
        remove { _engine.OnLevelChanged -= value; }
    }

    public int Level
    {
        get { return _engine.CurrentLevel; }
    }

    public int MaxLevel
    {
        get { return _engine.MaxLevel; }
    }

    [SerializeField]
    private LevelUpEngine _engine;

    public void Setup(int current, int max)
    {
        _engine.Setup(current, max);
    }

    public void SetLevel(int level)
    {
        _engine.CurrentLevel = level;
    }

    public void SetMaxLevel(int level)
    {
        _engine.MaxLevel = level;
    }

    public void AddLevel(int range)
    {
        _engine.CurrentLevel += range;
    }
}
