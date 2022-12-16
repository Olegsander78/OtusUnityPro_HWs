using System;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Move In Direction Engine")]
public sealed class MoveInDirectionEngine : MonoBehaviour
{
    private static readonly Vector3 ZERO_DIRECTION = Vector3.zero;

    public event Action OnStartMove;

    public event Action OnStopMove;

    public bool IsMoving
    {
        get { return this.moveRequired && this.direction != ZERO_DIRECTION; }
    }

    public Vector3 Direction
    {
        get { return this.direction; }
    }

    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    private bool moveRequired;

    [PropertyOrder(-9)]
    [ReadOnly]
    [ShowInInspector]
    private bool finishMove;

    [PropertyOrder(-8)]
    [ReadOnly]
    [ShowInInspector]
    private Vector3 direction;

    [Space]
    [SerializeField]
    private UpdateMode updateMode;

    [Space]
    [SerializeField]
    private MoveInDirecitonCondition[] preconditions;

    [SerializeField]
    private MoveInDirectionAction[] startActions;

    [SerializeField]
    private MoveInDirectionAction[] stopActions;

    private void FixedUpdate()
    {
        if (this.updateMode == UpdateMode.FIXED_UPDATE)
        {
            this.UpdateMove();
        }
    }

    private void Update()
    {
        if (this.updateMode == UpdateMode.UPDATE)
        {
            this.UpdateMove();
        }
    }

    public bool CanMove(Vector3 direction)
    {
        for (int i = 0, count = this.preconditions.Length; i < count; i++)
        {
            var condition = this.preconditions[i];
            if (!condition.IsTrue(direction))
            {
                return false;
            }
        }

        return true;
    }

    [Button]
    public void Move(Vector3 direction)
    {
        if (!this.CanMove(direction))
        {
            return;
        }

        this.direction = direction;
        this.finishMove = false;

        if (!this.moveRequired)
        {
            this.moveRequired = true;
            this.StartMove();
        }
    }

    private void StartMove()
    {
        for (int i = 0, count = this.startActions.Length; i < count; i++)
        {
            var action = this.startActions[i];
            action.Do(this.direction);
        }

        this.OnStartMove?.Invoke();
    }

    private void UpdateMove()
    {
        if (!this.moveRequired)
        {
            return;
        }

        if (this.finishMove)
        {
            this.moveRequired = false;
            this.StopMove();
        }

        this.finishMove = true;
    }

    [Button]
    private void StopMove()
    {
        for (int i = 0, count = this.stopActions.Length; i < count; i++)
        {
            var action = this.stopActions[i];
            action.Do(this.direction);
        }

        this.OnStopMove?.Invoke();
    }

    private enum UpdateMode
    {
        UPDATE = 0,
        FIXED_UPDATE = 1
    }
}