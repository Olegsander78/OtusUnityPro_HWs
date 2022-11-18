using UnityEngine;
using Elementary;
using Entities;

public class Projectile: MonoBehaviour
{
    public IntBehaviour Damage { get => _damage; set => value = _damage; }

    [SerializeField]
    private IntBehaviour _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<IEntity>() != null)
            {
                other.GetComponent<IEntity>().Get<IComponent_TakeDamage>().TakeDamage(_damage.Value);
                Destroy(gameObject);
            }            
        }
    }
}
