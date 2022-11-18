using Entities;
using UnityEngine;

public class MoveController : AbstractMoveController
{
    [SerializeField]
    private UnityEntityBase _unit;

    private IComponent_MoveInDirection _moveComponent;
    private void Awake()
    {
        _moveComponent = _unit.Get<IComponent_MoveInDirection>();
    }

    protected override void Move(Vector3 direction)
    {
        const float speed = 5.0f;
        Vector3 velocity = direction * (speed * Time.deltaTime);
        _moveComponent.Move(velocity);
    }
}
