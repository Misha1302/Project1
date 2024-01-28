namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float speed = 11.75f;

        private void OnCollisionEnter2D(Collision2D other) => OnCol(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCol(other.transform);


        private void OnCol(Component tr)
        {
            if (tr.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = rb.velocity.WithY(speed * (1 + (rb.gravityScale - 1) / 2));
        }
    }
}