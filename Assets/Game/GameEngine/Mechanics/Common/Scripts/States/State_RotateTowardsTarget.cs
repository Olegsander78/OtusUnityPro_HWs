using System.Collections;
using Elementary;
using UnityEngine;


public abstract class State_RotateTowardsTarget : MonoStateCoroutine
{
    [SerializeField]
    private FloatAdapter rotationSpeed; // 20.0f;

    [SerializeField]
    private TransformEngine engine;

    protected override IEnumerator Do()
    {
        while (true)
        {
            this.engine.RotateTowardsAtPosition(
                targetPosition: this.GetTargetPosition(),
                speed: this.rotationSpeed.Value,
                deltaTime: Time.deltaTime
            );
            yield return null;
        }
    }

    protected abstract Vector3 GetTargetPosition();
}