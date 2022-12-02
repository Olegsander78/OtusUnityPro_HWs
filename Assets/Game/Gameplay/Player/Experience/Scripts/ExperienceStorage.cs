using System;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("Gameplay/Player Experience Storage")]
public sealed class ExperienceStorage : MonoBehaviour
{
    public event Action<int> OnExpChanged;

    public event Action<int> OnExpEarned;

    public event Action<int> OnExpSpent;

    public int Experience
    {
        get { return _experience; }
    }

    [ReadOnly]
    [ShowInInspector]
    private int _experience;

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void EarnExperience(int amount)
    {
        if (amount == 0)
        {
            return;
        }

        if (amount < 0)
        {
            throw new Exception($"Can not earn negative Experience {amount}");
        }

        var previousValue = _experience;
        var newValue = previousValue + amount;

        _experience = newValue;
        OnExpChanged?.Invoke(newValue);
        OnExpEarned?.Invoke(amount);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void SpendExperience(int amount)
    {
        if (amount == 0)
        {
            return;
        }

        if (amount < 0)
        {
            throw new Exception($"Can not spend negative Experience {amount}");
        }

        var previousValue = _experience;
        var newValue = previousValue - amount;
        if (newValue < 0)
        {
            throw new Exception(
                $"Negative Experience after spend. Experience in bank: {previousValue}, spend amount {amount} ");
        }

        _experience = newValue;
        OnExpChanged?.Invoke(newValue);
        OnExpSpent?.Invoke(amount);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void SetupExperience(int money)
    {
        _experience = money;
        OnExpChanged?.Invoke(money);
    }

    public bool CanSpendExperience(int amount)
    {
        return _experience >= amount;
    }
}
