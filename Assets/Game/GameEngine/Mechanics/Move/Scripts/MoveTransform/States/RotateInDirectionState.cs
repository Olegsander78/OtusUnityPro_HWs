using Elementary;
using System.Collections;
using UnityEngine;


public class RotateInDirectionState : StateCoroutine
{
    [SerializeField]
    private MoveInDirectionEngine moveEngine;

    [SerializeField]
    private TransformEngine transformEngine;

    [SerializeField]
    private float rotationSpeed = 60;

    protected override IEnumerator Do()
    {
        var delay = new WaitForFixedUpdate();
        while (true)
        {
            yield return delay;
            this.RotateTransform();
        }
    }

    private void RotateTransform()
    {
        var direction = this.moveEngine.Direction;
        this.transformEngine.RotateTowardsInDirection(direction, this.rotationSpeed, Time.fixedDeltaTime);
    }
}