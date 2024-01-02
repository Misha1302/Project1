using UnityEngine;

public class Damage : MonoBehaviour
{
    public bool needToDestroy = true;
    public float damage = -1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        TryDamage(other.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other.transform);
    }

    public void TryDamage(Component other)
    {
        if (other.TryGetComponent<EnemyTag>(out _))
            return;

        if (other.TryGetComponent<Health>(out var health))
            health.Damage(damage);

        if (needToDestroy)
            Destroy(gameObject);
    }
}