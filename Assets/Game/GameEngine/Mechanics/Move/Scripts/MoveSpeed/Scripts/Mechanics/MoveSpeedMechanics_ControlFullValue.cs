using Elementary;
using UnityEngine;


public sealed class MoveSpeedMechanics_ControlFullValue : MonoBehaviour
{
    [SerializeField]
    private FloatBehaviour baseSpeed;

    [SerializeField]
    private FloatBehaviour multiplier;

    [Space]
    [SerializeField]
    private FloatBehaviour fullSpeed;

    private void Awake()
    {
        this.UpdateSpeed();
    }

    private void OnEnable()
    {
        this.baseSpeed.OnValueChanged += this.OnStateChanged;
        this.multiplier.OnValueChanged += this.OnStateChanged;
    }

    private void OnDisable()
    {
        this.baseSpeed.OnValueChanged -= this.OnStateChanged;
        this.multiplier.OnValueChanged -= this.OnStateChanged;
    }

    private void OnStateChanged(float _)
    {
        this.UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        var newSpeed = this.baseSpeed.Value * this.multiplier.Value;
        this.fullSpeed.Assign(newSpeed);
    }
}