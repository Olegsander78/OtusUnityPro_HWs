using Elementary;
using UnityEngine;


public sealed class AnimatorStateResolver : MonoBehaviour
{
    [SerializeField]
    private BoolBehaviour _isAttack;

    [SerializeField]
    private TakeDamageEngine _takeDamageEngine;

    [Space]
    [SerializeField]
    private AnimationSystem _animationSystem;

    private void Awake()
    {
        this.UpdateState(this._isAttack.Value);
    }

    private void OnEnable()
    {
        this._isAttack.OnValueChanged += this.OnStateChanged;
    }

    private void OnDisable()
    {
        this._isAttack.OnValueChanged -= this.OnStateChanged;
    }

    private void OnStateChanged(bool isTrue)
    {
        UpdateState(isTrue);
    }

    private void UpdateState(bool isTrue)
    {
        if (isTrue)
        {
            //this._animationSystem.SwitchState(AnimatorStateType.ATTACK);
        }
        else
        {
           // this._animationSystem.SwitchState(AnimatorStateType.IDLE);
        }
    }
}