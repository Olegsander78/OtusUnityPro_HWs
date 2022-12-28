using Elementary;
using UnityEngine;


public sealed class DeactivationMechanics_ActivateAfterCountdown : MonoBehaviour
{
    [SerializeField]
    private CountdownBehaviour countdown;

    [SerializeField]
    private ActivationBehaviour toggle;

    private void OnEnable()
    {
        this.toggle.OnDeactivate += this.OnDeactivate;
        this.countdown.OnEnded += this.OnActivate;
    }

    private void OnDisable()
    {
        this.toggle.OnDeactivate -= this.OnDeactivate;
        this.countdown.OnEnded -= this.OnActivate;
    }

    private void OnDeactivate()
    {
        this.countdown.ResetTime();
        this.countdown.Play();
    }

    private void OnActivate()
    {
        this.toggle.Activate();
    }
}