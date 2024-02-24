namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Bullet))]
    public class BulletDestroyer : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Bullet>().onCollision.AddListener(DestroyBullet);
        }

        private void DestroyBullet(Component _)
        {
            if (TryGetComponent<Collider2D>(out var col))
                col.enabled = false;

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            GameManager.Instance.CoroutineManager.StartCoroutine(
                CoroutineManager.InvokeAfterCoroutine(() => Destroy(gameObject), 5)
            );
        }
    }
}