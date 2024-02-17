namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.Events;

    public class Bullet : MonoBehaviour
    {
        public readonly UnityEvent<Component> onCollision = new();

        private void OnCollisionEnter2D(Collision2D other) => TryDamage(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => TryDamage(other.transform);

        private void TryDamage(Component other)
        {
            if (NeedToBeIgnored(other))
                return;

            onCollision.Invoke(other);
        }

        private bool NeedToBeIgnored(Component other) =>
            other.TryGetComponent<IgnoreCollisionTag>(out _);
    }
}