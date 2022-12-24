using System;
using UnityEngine;


public sealed class EnemyStateResolver : MonoBehaviour
{
    [SerializeField]
    private EnemyStateMachine _stateMachine;

    [Space]
    [SerializeField]
    private DestroyReceiver _dieReceiver;

    [SerializeField]
    private TakeDamageEngine _takeDamageEngine;

    //[SerializeField]
    //private MeleeCombatEngine combatEngine;

    //[SerializeField]
    //private MoveInDirectionEngine moveEngine;

    //[SerializeField]
    //private EventBehaviour respawnReceiver;

    private void OnEnable()
    {
        //this.moveEngine.OnStartMove += this.OnMoveStarted;
        //this.moveEngine.OnStopMove += this.OnMoveEnded;

        //this.combatEngine.OnCombatStarted += this.OnCombatStarted;
        //this.combatEngine.OnCombatStopped += this.OnCombatEnded;

        _dieReceiver.OnDestroy += OnDestroyed;
        _takeDamageEngine.OnDamageTaken += OnDamageTakenStarted;
        _takeDamageEngine.OnDamageTakenFinished += OnDamageTakenFinished;
        //this.respawnReceiver.OnEvent += this.OnRespawned;
    }    

    private void OnDisable()
    {
        //this.moveEngine.OnStartMove += this.OnMoveStarted;
        //this.moveEngine.OnStopMove += this.OnMoveEnded;

        //this.combatEngine.OnCombatStarted += this.OnCombatStarted;
        //this.combatEngine.OnCombatStopped += this.OnCombatEnded;

        _dieReceiver.OnDestroy -= OnDestroyed;
        _takeDamageEngine.OnDamageTaken -= OnDamageTakenStarted;
        _takeDamageEngine.OnDamageTakenFinished -= OnDamageTakenFinished;
        //this.respawnReceiver.OnEvent -= this.OnRespawned;
    }

    //private void OnMoveStarted()
    //{
    //    this.stateMachine.SwitchState(EnemyStateType.MOVE);
    //}

    //private void OnMoveEnded()
    //{
    //    if (this.stateMachine.CurrentState == EnemyStateType.MOVE)
    //    {
    //        this.stateMachine.SwitchState(EnemyStateType.IDLE);
    //    }
    //}

    //private void OnCombatStarted(MeleeCombatOperation operation)
    //{
    //    this.stateMachine.SwitchState(EnemyStateType.MELEE_COMBAT);
    //}

    //private void OnCombatEnded(MeleeCombatOperation operation)
    //{
    //    if (this.stateMachine.CurrentState == EnemyStateType.MELEE_COMBAT)
    //    {
    //        this.stateMachine.SwitchState(EnemyStateType.IDLE);
    //    }
    //}

    private void OnDestroyed(DestroyEvent @event)
    {
        _stateMachine.SwitchState(EnemyStateType.DIE);
    }

    private void OnDamageTakenStarted(TakeDamageEvent obj)
    {
        _stateMachine.SwitchState(EnemyStateType.HIT);        
    }

    private void OnDamageTakenFinished()
    {
        if (_stateMachine.CurrentState == EnemyStateType.HIT)
        {
            _stateMachine.SwitchState(EnemyStateType.IDLE);
        }
    }


    //private void OnRespawned()
    //{
    //    if (this.stateMachine.CurrentState == EnemyStateType.DIE)
    //    {
    //        this.stateMachine.SwitchState(EnemyStateType.IDLE);
    //    }
    //}
}