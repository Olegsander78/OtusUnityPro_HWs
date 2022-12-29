using UnityEngine;
using Elementary;

public abstract class TakeDamageMechanics : MonoBehaviour
{
    [SerializeField]
    private TakeDamageEngine _takeDamageEngine;

    private void OnEnable()
    {
        _takeDamageEngine.OnDamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        _takeDamageEngine.OnDamageTaken -= OnDamageTaken;
    }

    protected abstract void OnDamageTaken(TakeDamageEvent damageEvent);
}
