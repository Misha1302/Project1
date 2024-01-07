namespace NamehaveCat.Scripts.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = -1;
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other) => TryDamage(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => TryDamage(other.transform);

        public void TryDamage(Component other)
        {
            var health = other.GetComponentInParent<Health>();
            if (health != null)
                health.Damage(damage, message);

            if(TryGetComponent<BulletAnimator>(out var anim))
                anim.DestroyBullet();
            else Destroy(gameObject);
        }
    }
}