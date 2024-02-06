namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class Bullet : DamageableBase
    {
        [SerializeField] private float damage = -1;
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other) => TryDamage(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => TryDamage(other.transform);

        private void TryDamage(Component other)
        {
            var health = other.GetComponentInParent<Health>();
            if (health != null)
                health.Damage(damage, message);

            if (other.TryGetComponent<IgnoreCollisionTag>(out _))
                return;

            if (TryGetComponent<BulletMAnimator>(out var anim))
                anim.DestroyBullet();
            else Destroy(gameObject);
        }
    }
}