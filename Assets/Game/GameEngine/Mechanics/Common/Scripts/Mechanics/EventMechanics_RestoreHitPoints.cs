using UnityEngine;


public sealed class EventMechanics_RestoreHitPoints : EventMechanics
{
    [SerializeField]
    private HitPointsEngine hitPointsEngine;

    protected override void OnEvent()
    {
        this.hitPointsEngine.CurrentHitPoints = this.hitPointsEngine.MaxHitPoints;
    }
}