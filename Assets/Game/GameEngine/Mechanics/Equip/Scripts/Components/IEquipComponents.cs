using System;


public interface IComponent_Equip
{
    void Equip();
}
public interface IComponent_OnEquipped
{
    event Action OnEquipped;
}

public interface IComponent_GetEqupType
{
    EquipType Type { get; }
}