using UnityEngine;

namespace NamehaveCat.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = -1;
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other) => TryDamage(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => TryDamage(other.transform);

        public void TryDamage(Component other)
        {
            if (other.TryGetComponent<Health>(out var health))
                health.Damage(damage, message);

            Destroy(gameObject);
        }
    }
}