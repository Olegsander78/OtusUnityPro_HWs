using Elementary;
using UnityEngine;


public sealed class MoveInDirectionMechanics : MonoBehaviour
{
    [SerializeField]
    private MoveInDirectionEngine moveEngine;

    [SerializeField]
    private TransformEngine transformEngine;

    [SerializeField]
    private FloatAdapter moveSpeed;

    private void FixedUpdate()
    {
        if (this.moveEngine.IsMoving)
        {
            this.MoveTransform(this.moveEngine.Direction);
        }
    }

    private void MoveTransform(Vector3 direction)
    {
        var velocity = direction * (this.moveSpeed.Value * Time.fixedDeltaTime);
        this.transformEngine.MovePosition(velocity);
        this.transformEngine.LookInDirection(direction);
    }
}