using UnityEngine;
using Elementary;
using Entities;
using Unity.VisualScripting;

public class Projectile: MonoBehaviour
{
    public IntBehaviour Damage { get => _damage; set => value = _damage; }

    [SerializeField]
    private IntBehaviour _damage;

    [SerializeField]
    private IEntity _target;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other)
    {       
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<IEntity>() != null)
            {
                _target= other.GetComponent<IEntity>();
                DealDamage();
                transform.parent = other.transform;
                _rigidbody.velocity = transform.forward * 0f;
            }
        }
    }

    public void DealDamage()
    {   
        var aliveComponent = _target.Get<IComponent_IsAlive>();
        if (!aliveComponent.IsAlive)
        {
            return;
        }

        var takeDamageComponent = _target.Get<IComponent_TakeDamage>();
        var damageEvent = new TakeDamageEvent(
            _damage.Value,
            TakeDamageReason.BULLET,
            this
        );
        takeDamageComponent.TakeDamage(damageEvent);
    }
}
