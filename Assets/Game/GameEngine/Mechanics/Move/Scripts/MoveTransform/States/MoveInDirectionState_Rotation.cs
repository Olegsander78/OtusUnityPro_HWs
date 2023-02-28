using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class MoveInDirectionState_Rotation : State
{
    [SerializeField]
    private MoveInDirectionEngine moveEngine;

    [SerializeField]
    private TransformEngine transformEngine;

    [Space]
    [SerializeField]
    private Mode mode = Mode.INSTANTLY;

    [ShowIf("mode", Mode.SMOOTH)]
    [SerializeField]
    private FloatAdapter speed; //45

    private void Awake()
    {
        this.enabled = false;
    }

    private void Update()
    {
        if (this.moveEngine.IsMoving)
        {
            this.RotateInDirection();
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

    private void RotateInDirection()
    {
        var direction = this.moveEngine.Direction;
        if (this.mode == Mode.INSTANTLY)
        {
            this.transformEngine.LookInDirection(direction);
        }
        else if (this.mode == Mode.SMOOTH)
        {
            this.transformEngine.RotateTowardsInDirection(direction, this.speed.Value, Time.deltaTime);
        }
    }

    private enum Mode
    {
        INSTANTLY = 0,
        SMOOTH = 1
    }
}