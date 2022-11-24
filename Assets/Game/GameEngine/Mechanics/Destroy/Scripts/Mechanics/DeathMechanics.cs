using UnityEngine;
using Elementary;

public class DeathMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _deathReceiver;

    [SerializeField]
    private IntBehaviour _hitPoints;

    private void OnEnable()
    {
        _hitPoints.OnValueChanged += OnHitPointsChanged;
        _deathReceiver.OnEvent += OnDestroyEvent;
    }    

    private void OnDisable()
    {
        _hitPoints.OnValueChanged -= OnHitPointsChanged;
        _deathReceiver.OnEvent -= OnDestroyEvent;
    }
    private void OnHitPointsChanged(int newHitPoints)
    {
        if(newHitPoints <= 0)
        {
            _deathReceiver.Call();
        }
    }

    private void OnDestroyEvent()
    {
        _deathReceiver.Call();
    }
}
