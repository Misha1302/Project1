namespace NamehaveCat.Scripts.Machinery
{
    using System;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;
    using UnityEngine.Events;

    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float height = 5f;

        public readonly UnityEvent<Trampoline> onCollision = new();

        private void OnCollisionEnter2D(Collision2D other) => OnCol(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCol(other.transform);


        private void OnCol(Component tr)
        {
            onCollision.Invoke(this);

            if (tr.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = rb.velocity.WithY(MathF.Sqrt(2 * height * 10 * rb.gravityScale));
        }
    }
}