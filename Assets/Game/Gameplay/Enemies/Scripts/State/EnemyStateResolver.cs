using UnityEngine;
using Elementary;

public sealed class EnemyStateResolver : MonoBehaviour
{
    [SerializeField]
    private EnemyStateMachine _stateMachine;

    [Space]
    [SerializeField]
    private DestroyReceiver _dieReceiver;

    //[SerializeField]
    //private TakeDamageEngine _takeDamageEngine;

    [SerializeField]
    private MeleeCombatEngine _combatEngine;

    [SerializeField]
    private MoveInDirectionEngine _moveEngine;

    [SerializeField]
    private EventBehaviour _respawnReceiver;

    private void OnEnable()
    {
        _dieReceiver.OnDestroy += OnDestroyed;
        _respawnReceiver.OnEvent += OnRespawned;

        _moveEngine.OnStartMove += OnMoveStarted;
        _moveEngine.OnStopMove += OnMoveEnded;

        _combatEngine.OnCombatStarted += OnCombatStarted;
        _combatEngine.OnCombatStopped += OnCombatEnded;


        //_takeDamageEngine.OnDamageTaken += OnDamageTakenStarted;
        //_takeDamageEngine.OnDamageTakenFinished += OnDamageTakenFinished;
    }    

    private void OnDisable()
    {
        _dieReceiver.OnDestroy -= OnDestroyed;
        _respawnReceiver.OnEvent -= OnRespawned;

        _moveEngine.OnStartMove -= OnMoveStarted;
        _moveEngine.OnStopMove -= OnMoveEnded;

        _combatEngine.OnCombatStarted -= OnCombatStarted;
        _combatEngine.OnCombatStopped -= OnCombatEnded;


        //_takeDamageEngine.OnDamageTaken -= OnDamageTakenStarted;
        //_takeDamageEngine.OnDamageTakenFinished -= OnDamageTakenFinished;
    }
    private void OnDestroyed(DestroyEvent @event)
    {
        _stateMachine.SwitchState(EnemyStateType.DIE);
    }
    private void OnRespawned()
    {
        if (_stateMachine.CurrentState == EnemyStateType.DIE)
        {
            _stateMachine.SwitchState(EnemyStateType.IDLE);
        }
    }

    private void OnMoveStarted()
    {
        _stateMachine.SwitchState(EnemyStateType.MOVE);
    }

    private void OnMoveEnded()
    {
        if (_stateMachine.CurrentState == EnemyStateType.MOVE)
        {
            _stateMachine.SwitchState(EnemyStateType.IDLE);
        }
    }

    private void OnCombatStarted(MeleeCombatOperation operation)
    {
        _stateMachine.SwitchState(EnemyStateType.MELEE_COMBAT);
    }

    private void OnCombatEnded(MeleeCombatOperation operation)
    {
        if (_stateMachine.CurrentState == EnemyStateType.MELEE_COMBAT)
        {
            _stateMachine.SwitchState(EnemyStateType.IDLE);
        }
    }


    //private void OnDamageTakenStarted(TakeDamageEvent obj)
    //{
    //    _stateMachine.SwitchState(EnemyStateType.HIT);
    //    Debug.Log($"ENTER STATE {EnemyStateType.HIT}");

    //    //if (obj.source == null)
    //    //{
    //    //    _stateMachine.SwitchState(EnemyStateType.IDLE);
    //    //    Debug.Log($"EXIT STATE {EnemyStateType.HIT}");
    //    //}

    //    if (_stateMachine.CurrentState == EnemyStateType.HIT)
    //    {
    //        _stateMachine.SwitchState(EnemyStateType.IDLE);
    //        Debug.Log($"EXIT STATE {EnemyStateType.HIT}");
    //    }
    //}

    //private void OnDamageTakenFinished()
    //{
    //    if (_stateMachine.CurrentState == EnemyStateType.HIT)
    //    {
    //        _stateMachine.SwitchState(EnemyStateType.IDLE);
    //    }
    //}
}