using System;
using UnityEngine;


public interface IComponent_UnloadZone
{
    event Action<int> OnAmountChanged;

    int MaxAmount { get; }

    int CurrentAmount { get; }

    bool IsFull { get; }

    bool IsEmpty { get; }

    ResourceType ResourceType { get; }

    Vector3 ParticlePosition { get; }

    void SetupAmount(int currentAmount);

    int ExtractAll();
}