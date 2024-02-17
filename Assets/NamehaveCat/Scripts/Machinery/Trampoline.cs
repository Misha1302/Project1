namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;
    using UnityEngine.Events;

    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float speed = 11.75f;

        public readonly UnityEvent<Trampoline> onCollision = new();

        private void OnCollisionEnter2D(Collision2D other) => OnCol(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCol(other.transform);


        private void OnCol(Component tr)
        {
            onCollision.Invoke(this);

            if (tr.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = rb.velocity.WithY(speed * (1 + (rb.gravityScale - 1) / 2));
        }
    }
}