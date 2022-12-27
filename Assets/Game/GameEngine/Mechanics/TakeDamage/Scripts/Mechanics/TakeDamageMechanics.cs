using UnityEngine;
using Elementary;

public class TakeDamageMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Int _takeDamageReceiver;

    [SerializeField]
    private IntBehaviour _hitPoints;

    private void OnEnable()
    {
        _takeDamageReceiver.OnEvent += OnDamageTaken;
    }

    private void OnDisable()
    {
        _takeDamageReceiver.OnEvent -= OnDamageTaken;
    }

    private void OnDamageTaken(int damage)
    {
        _hitPoints.Value -= damage;
    }
}
