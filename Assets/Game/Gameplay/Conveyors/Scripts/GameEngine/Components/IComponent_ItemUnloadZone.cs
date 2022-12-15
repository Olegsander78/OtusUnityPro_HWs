using System;
using UnityEngine;


public interface IComponent_ItemUnloadZone
{
    event Action<int> OnAmountChanged;

    int MaxAmount { get; }

    int CurrentAmount { get; }

    bool IsFull { get; }

    bool IsEmpty { get; }

    ItemFromTwoResourcesInfo ItemFromTwoResources { get; set; }

    Vector3 ParticlePosition { get; }

    void SetupAmount(int currentAmount);

    int ExtractAll();
}