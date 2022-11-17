using UnityEngine;
using Elementary;

public class Projectile: MonoBehaviour
{
    public IntBehaviour Damage { get => _damage; set => value = _damage; }

    [SerializeField]
    private IntBehaviour _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyDummy>().TakeDamage(_damage.Value);
            Destroy(gameObject);
        }
    }
}
