namespace NamehaveCat.Scripts.Player
{
    using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class GroundChecker : MonoBehaviour
    {
        private BoxCollider2D _mainCollider;
        public bool IsGrounded => GetIsGrounded();

        private void Start()
        {
            _mainCollider = GetComponent<BoxCollider2D>();
        }

        private bool GetIsGrounded()
        {
            var colliders = GetColliders();

            var isGrounded = false;
            foreach (var t in colliders)
                if (!t.transform.CompareTag("Player"))
                {
                    isGrounded = true;
                    break;
                }

            return isGrounded;
        }

        private Collider2D[] GetColliders()
        {
            var colliderBounds = _mainCollider.bounds;
            var colliderRadius = _mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
            var groundCheckPos =
                colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);

            // ReSharper disable once Unity.PreferNonAllocApi
            var colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
            return colliders;
        }
    }
}