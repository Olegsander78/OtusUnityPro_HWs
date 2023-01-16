using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Hit Points Bar Adapter")]
public sealed class HitPointsBarAdapter : MonoBehaviour
{
    [SerializeField]
    private HitPointsEngine hitPointsEngine;

    [SerializeField]
    private HitPointsBar view;

    private void Awake()
    {
        this.SetupBar();
    }

    private void OnEnable()
    {
        this.hitPointsEngine.OnHitPointsChanged += this.OnHitPointsChanged;
        this.hitPointsEngine.OnMaxHitPointsChanged += this.OnHitPointsChanged;
    }

    private void OnDisable()
    {
        this.hitPointsEngine.OnHitPointsChanged -= this.OnHitPointsChanged;
        this.hitPointsEngine.OnMaxHitPointsChanged -= this.OnHitPointsChanged;
    }

    private void OnHitPointsChanged(int hitPoints)
    {
        this.UpdateBar(hitPoints);
    }

    private void SetupBar()
    {
        var hitPoints = this.hitPointsEngine.CurrentHitPoints;
        var maxHitPoints = this.hitPointsEngine.MaxHitPoints;

        var showBar = hitPoints > 0;
        this.view.SetVisible(showBar);
        this.view.SetHitPoints(hitPoints, maxHitPoints);
    }

    private void UpdateBar(int hitPoints)
    {
        var maxHitPoints = this.hitPointsEngine.MaxHitPoints;
        var showBar = hitPoints > 0;

        this.view.SetVisible(showBar);
        this.view.SetHitPoints(hitPoints, maxHitPoints);
    }
}