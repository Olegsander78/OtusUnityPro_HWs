using System.Collections;
using Elementary;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class State_CheckDistanceToTarget : MonoStateCoroutine
{
    [Space]
    [SerializeField]
    [FormerlySerializedAs("engine")]
    private TransformEngine transformEngine;

    [SerializeField]
    private FloatAdapter minDistance; // 1.2f;

    protected override IEnumerator Do()
    {
        var period = new WaitForFixedUpdate();
        while (true)
        {
            var targetPosiiton = this.GetTargetPosition();
            var distanceReached = this.transformEngine.IsDistanceReached(targetPosiiton, this.minDistance.Value);
            this.OnUpdate(distanceReached);
            yield return period;
        }
    }

    protected abstract void OnUpdate(bool distanceReached);

    protected abstract Vector3 GetTargetPosition();
}