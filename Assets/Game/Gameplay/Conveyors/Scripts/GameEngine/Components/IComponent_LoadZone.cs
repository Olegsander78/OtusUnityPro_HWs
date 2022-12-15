using System;

public interface IComponent_LoadZone
{
    event Action<int> OnAmountChanged;

    int MaxAmount { get; }

    int CurrentAmount { get; }

    int AvailableAmount { get; }

    bool IsFull { get; }

    ResourceType ResourceType { get; set; }

    void SetupAmount(int currentAmount);

    void PutAmount(int range);
}