using System;
using System.Collections.Generic;


public interface IResourceSource
{
    event Action<ResourceType, int> OnValueChanged;

    event Action OnSetuped;

    event Action OnCleared;

    int Count { get; }

    int this[ResourceType type] { get; }

    ResourceData[] GetAll();

    void GetAllNonAlloc(Dictionary<ResourceType, int> result);

    void GetAllNonAlloc(List<ResourceData> result);

    bool Exists(ResourceType type, int requiredCount);

    void Plus(ResourceType type, int range);

    void Minus(ResourceType type, int range);

    void Clear();
}