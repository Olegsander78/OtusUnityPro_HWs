using Elementary;
using UnityEngine;


public sealed class MoveInDirectionState_Position : State
{
    [SerializeField]
    private MoveInDirectionEngine moveEngine;

    [SerializeField]
    private TransformEngine transformEngine;

    [SerializeField]
    private FloatAdapter speed;

    private void Awake()
    {
        this.enabled = false;
    }

    private void FixedUpdate()
    {
        if (this.moveEngine.IsMoving)
        {
            this.MoveInDirection();
        }
    }

    public override void Enter()
    {
        this.enabled = true;
    }

    public override void Exit()
    {
        this.enabled = false;
    }

    private void MoveInDirection()
    {
        var velocity = this.moveEngine.Direction * (this.speed.Value * Time.fixedDeltaTime);
        this.transformEngine.MovePosition(velocity);
    }
}