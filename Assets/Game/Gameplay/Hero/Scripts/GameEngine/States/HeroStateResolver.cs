using UnityEngine;
using Elementary;

public sealed class HeroStateResolver : MonoBehaviour
{
    [SerializeField]
    private HeroStateMachine _stateMachine;

    [Space]
    [SerializeField]
    private MoveInDirectionEngine _moveEngine;

    [SerializeField]
    private RangeAttackEngine _rangeAttackEngine;

    [SerializeField]
    private ProjectileEngine _projectileEngine;

    [SerializeField]
    private HarvestResourceEngineLS _harvestEngine;

    [SerializeField]
    private MeleeCombatEngine _combatEngine;

    //[SerializeField]
    //private HarvestResourceEngine harvestEngine;

    //[SerializeField]
    //private DestroyReceiver destroyReceiver;

    //[SerializeField]
    //private EventBehaviour respawnReceiver;

    private void OnEnable()
    {
        _moveEngine.OnStartMove += OnMoveStarted;
        _moveEngine.OnStopMove += OnMoveEnded;

        _rangeAttackEngine.OnRangeAttackStarted += OnShootStarted;
        _rangeAttackEngine.OnRangeAttackFinished += OnShootEnded;

        _harvestEngine.OnStarted += OnHarvestStarted;
        _harvestEngine.OnStopped += OnHarvestFinished;

        _combatEngine.OnCombatStarted += OnCombatStarted;
        _combatEngine.OnCombatStopped += OnCombatEnded;

        //this.harvestEngine.OnHarvestStarted += this.OnHarvestStarted;
        //this.harvestEngine.OnHarvestStopped += this.OnHarvestEnded;

        //this.destroyReceiver.OnDestroy += this.OnDied;
        //this.respawnReceiver.OnEvent += this.OnRespawned;
    }

    private void OnDisable()
    {
        _moveEngine.OnStartMove -= OnMoveStarted;
        _moveEngine.OnStopMove -= OnMoveEnded;

        _rangeAttackEngine.OnRangeAttackStarted -= OnShootStarted;
        _rangeAttackEngine.OnRangeAttackFinished -= OnShootEnded;

        _harvestEngine.OnStarted -= OnHarvestStarted;
        _harvestEngine.OnStopped -= OnHarvestFinished;

        _combatEngine.OnCombatStarted -= OnCombatStarted;
        _combatEngine.OnCombatStopped -= OnCombatEnded;

        //this.harvestEngine.OnHarvestStarted -= this.OnHarvestStarted;
        //this.harvestEngine.OnHarvestStopped -= this.OnHarvestEnded;

        //this.destroyReceiver.OnDestroy -= this.OnDied;
        //this.respawnReceiver.OnEvent -= this.OnRespawned;
    }

    #region MechanicsEvents

    //private void OnDied(DestroyEvent destroyEvent)
    //{
    //    this.stateMachine.SwitchState(HeroStateType.DEATH);
    //}

    private void OnMoveStarted()
    {
        _stateMachine.SwitchState(HeroStateType.RUN);
    }

    private void OnMoveEnded()
    {
        if (_stateMachine.CurrentState == HeroStateType.RUN)
        {
            _stateMachine.SwitchState(HeroStateType.IDLE);
        }
    }

    private void OnShootStarted()
    {
        _stateMachine.SwitchState(HeroStateType.SHOOT);
        //Debug.LogWarning("SHOOT STATE ENTER");
    }
    private void OnShootEnded()
    {
        if (_stateMachine.CurrentState == HeroStateType.SHOOT)
        {
            _stateMachine.SwitchState(HeroStateType.IDLE); 
                
            //Debug.LogWarning("SHOOT STATE EXIT");
        }
    }

    private void OnHarvestStarted(HarvestResourceOperation operation)
    {
        if (_stateMachine.CurrentState is HeroStateType.IDLE or HeroStateType.RUN)
        {
            _stateMachine.SwitchState(HeroStateType.HARVEST_RESOURCE);
        }
    }

    private void OnHarvestFinished(HarvestResourceOperation operation)
    {
        if (_stateMachine.CurrentState == HeroStateType.HARVEST_RESOURCE)
        {
            _stateMachine.SwitchState(HeroStateType.IDLE);
        }
    }    

    private void OnCombatStarted(MeleeCombatOperation operation)
    {
        _stateMachine.SwitchState(HeroStateType.MELEE_COMBAT);
    }

    private void OnCombatEnded(MeleeCombatOperation operation)
    {
        if (_stateMachine.CurrentState == HeroStateType.MELEE_COMBAT)
        {
            _stateMachine.SwitchState(HeroStateType.IDLE);
        }
    }

    //private void OnHarvestStarted(HarvestResourceOperation operation)
    //{
    //    this.stateMachine.SwitchState(HeroStateType.HARVEST_RESOURCE);
    //}

    //private void OnHarvestEnded(HarvestResourceOperation operation)
    //{
    //    if (this.stateMachine.CurrentState == HeroStateType.HARVEST_RESOURCE)
    //    {
    //        this.stateMachine.SwitchState(HeroStateType.IDLE);
    //    }
    //}

    //private void OnRespawned()
    //{
    //    if (this.stateMachine.CurrentState == HeroStateType.DEATH)
    //    {
    //        this.stateMachine.SwitchState(HeroStateType.IDLE);
    //    }
    //}

    #endregion
}