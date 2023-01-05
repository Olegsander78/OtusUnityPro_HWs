using System.Collections;
using Elementary;
using UnityEngine;


public sealed class HarvestResourceState_ByTimer : StateCoroutine
{
    [SerializeField]
    private HarvestResourceEngineLS engine;

    [SerializeField]
    private FloatAdapter duration;

    protected override IEnumerator Do()
    {
        yield return new WaitForSeconds(this.duration.Value);
        this.engine.CurrentOperation.IsCompleted = true;
        this.engine.StopHarvest();
    }
}