using Elementary;
using UnityEngine;


public sealed class MeleeDamageMechanics_ControlFullDamage : MonoBehaviour
{
    [SerializeField]
    private IntBehaviour baseDamage;

    [SerializeField]
    private FloatBehaviour multiplier;

    [SerializeField]
    private IntBehaviour fullDamage;

    private void Awake()
    {
        this.UpdateDamage();
    }

    private void OnEnable()
    {
        this.baseDamage.OnValueChanged += this.OnDamageChanged;
        this.multiplier.OnValueChanged += this.OnMultiplierChanged;
    }

    private void OnDisable()
    {
        this.baseDamage.OnValueChanged -= this.OnDamageChanged;
        this.multiplier.OnValueChanged -= this.OnMultiplierChanged;
    }

    private void OnMultiplierChanged(float _)
    {
        this.UpdateDamage();
    }

    private void OnDamageChanged(int _)
    {
        var newDamage = this.EvaluateDamage();
        this.fullDamage.Assign(newDamage);
    }

    private void UpdateDamage()
    {
        var newDamage = this.EvaluateDamage();
        this.fullDamage.Assign(newDamage);
    }

    private int EvaluateDamage()
    {
        var damage = this.baseDamage.Value * this.multiplier.Value;
        return Mathf.RoundToInt(damage);
    }
}