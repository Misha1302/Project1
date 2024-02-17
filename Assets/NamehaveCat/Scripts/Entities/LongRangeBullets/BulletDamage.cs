namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Health;
    using UnityEngine;

    public class BulletDamage : DamageableBase
    {
        [SerializeField] private DamageInfo damage;

        private void Start()
        {
            GetComponent<Bullet>().onCollision.AddListener(other =>
            {
                var health = other.GetComponentInParent<Health>();

                if (health != null)
                    health.Damage(damage);
            });
        }
    }
}