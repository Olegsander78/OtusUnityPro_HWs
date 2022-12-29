using Elementary;
using Unity.VisualScripting;
using UnityEngine;


public sealed class EnemyAnimatorStateResolver : MonoBehaviour
{
    [Space]
    [SerializeField]
    private AnimationSystem _animationSystem;

    [SerializeField]
    private TakeDamageEngine _takeDamageEngine;

    [SerializeField]
    private DestroyReceiver _destroyReceiver;

    [SerializeField]
    private BoolBehaviour _isEnable;


    private void Awake()
    {
        UpdateStateEnable(_isEnable.Value);
    }

    private void OnEnable()
    {
        _isEnable.OnValueChanged += OnEnabled;
        _destroyReceiver.OnDestroy += OnDestroyed;
        _takeDamageEngine.OnDamageTaken += OnHit;
    }

    private void OnDisable()
    {
        _isEnable.OnValueChanged -= OnEnabled;
        _destroyReceiver.OnDestroy -= OnDestroyed;
        _takeDamageEngine.OnDamageTaken -= OnHit;
    }

    private void OnDestroyed(DestroyEvent destroyEvent)
    {
        UpdateStateDestroy(destroyEvent);
    }

    private void OnHit(TakeDamageEvent takeDamageEvent)
    {
        UpdateStateHit(takeDamageEvent);
    }

    private void OnEnabled(bool isTrue)
    {
        UpdateStateEnable(isTrue);
    }

    private void UpdateStateEnable (bool isTrue)
    {
        if (_isEnable == isTrue)
        {
            _animationSystem.ChangeState((int)EnemyAnimatorStateType.IDLE);
        }
    }
    private void UpdateStateDestroy(DestroyEvent destroyEvent)
    {
        _animationSystem.ChangeState((int)EnemyAnimatorStateType.DIE);
        Debug.LogWarning($"{EnemyAnimatorStateType.DIE} ANIM STATE ENTER");

        //if (destroyEvent.source != null)
        //{
        //    _animationSystem.ChangeState((int)EnemyAnimatorStateType.DIE);
        //    Debug.LogWarning($"{EnemyAnimatorStateType.DIE} ANIM STATE ENTER");
        //}
        //else
        //{
        //    _animationSystem.ChangeState((int)EnemyAnimatorStateType.IDLE);
        //    Debug.LogWarning($"{EnemyAnimatorStateType.DIE} ANIM STATE EXIT");
        //}
    }
    private void UpdateStateHit(TakeDamageEvent takeDamageEvent)
    {
       
        _animationSystem.PlayAnimation("GetHit", "BaseLayer", 0);

        _animationSystem.ChangeState((int)EnemyAnimatorStateType.IDLE);
        
        //if(takeDamageEvent.source != null)
        //{
        //    Debug.Log($"{takeDamageEvent.source}, {takeDamageEvent.reason}");
        //    _animationSystem.ChangeState((int)EnemyAnimatorStateType.HIT);
        //    Debug.LogWarning($"{EnemyAnimatorStateType.HIT} ANIM STATE ENTER , {takeDamageEvent.source}");            
        //}
        //else
        //{
        //    _animationSystem.ChangeState((int)EnemyAnimatorStateType.IDLE); 
        //    Debug.LogWarning($"{EnemyAnimatorStateType.HIT} ANIM STATE EXIT, {takeDamageEvent.source}");
        //}
    }
}