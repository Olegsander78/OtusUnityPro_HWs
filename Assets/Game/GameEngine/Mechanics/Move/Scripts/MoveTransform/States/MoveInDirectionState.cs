using System.Collections;
using Elementary;
using UnityEngine;


public sealed class MoveInDirectionState : StateCoroutine
{
    [SerializeField]
    private MoveInDirectionEngine moveEngine;

    [SerializeField]
    private TransformEngine transformEngine;

    [SerializeField]
    private FloatAdapter moveSpeed;

    protected override IEnumerator Do()
    {
        var delay = new WaitForFixedUpdate();
        while (true)
        {
            yield return delay;
            this.MoveTransform();
        }
    }

    private void MoveTransform()
    {
        var direction = this.moveEngine.Direction;
        var velocity = direction * (this.moveSpeed.Value * Time.fixedDeltaTime);
        this.transformEngine.MovePosition(velocity);
    }
}