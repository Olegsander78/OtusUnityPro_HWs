using System;


public interface IComponent_MeleeCombat
{
    event Action<MeleeCombatOperation> OnCombatStarted;

    event Action<MeleeCombatOperation> OnCombatStopped;

    bool IsCombat { get; }

    bool CanStartCombat(MeleeCombatOperation operation);

    void StartCombat(MeleeCombatOperation operation);

    void StopCombat();
}