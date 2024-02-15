namespace NamehaveCat.Scripts.Machinery
{
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class StickyPlatform : MonoBehaviour
    {
        [SerializeField] private Vector2 size = Vector2.one;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Rigidbody2D>(out _))
                other.transform.parent = transform;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Rigidbody2D>(out _))
                other.transform.parent = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, size);
        }
    }
}