using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Melee Combat»")]
public sealed class Component_MeleeCombat : MonoBehaviour, IComponent_MeleeCombat
{
    public event Action<MeleeCombatOperation> OnCombatStarted
    {
        add { this.combatEngine.OnCombatStarted += value; }
        remove { this.combatEngine.OnCombatStarted -= value; }
    }

    public event Action<MeleeCombatOperation> OnCombatStopped
    {
        add { this.combatEngine.OnCombatStopped += value; }
        remove { this.combatEngine.OnCombatStopped -= value; }
    }

    public bool IsCombat
    {
        get { return this.combatEngine.IsCombat; }
    }

    [SerializeField]
    private MeleeCombatEngine combatEngine;

    public bool CanStartCombat(MeleeCombatOperation operation)
    {
        return this.combatEngine.CanStartCombat(operation);
    }

    public void StartCombat(MeleeCombatOperation operation)
    {
        this.combatEngine.StartCombat(operation);
    }

    public void StopCombat()
    {
        this.combatEngine.StopCombat();
    }
}