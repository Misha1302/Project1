namespace NamehaveCat.Scripts.Player
{
    using System;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class GroundChecker : MonoBehaviour
    {
        public bool IsGrounded => GetIsGrounded();

        private void Start()
        {
            if (GetComponent<BoxCollider2D>().size != Vector2.one)
                throw new InvalidOperationException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, transform.lossyScale);
        }

        private bool GetIsGrounded()
        {
            var colliders = GetColliders();

            var isGrounded = false;
            foreach (var t in colliders)
                if (!t.TryGetComponent<PlayerTag>(out _))
                {
                    isGrounded = true;
                    break;
                }

            return isGrounded;
        }

        // ReSharper disable once Unity.PreferNonAllocApi
        private Collider2D[] GetColliders() =>
            Physics2D.OverlapBoxAll(transform.position, transform.lossyScale, 0);
    }
}