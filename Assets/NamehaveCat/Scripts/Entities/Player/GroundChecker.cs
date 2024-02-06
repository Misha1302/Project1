namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class GroundChecker : MonoBehaviour
    {
        private readonly Collider2D[] _results = new Collider2D[128];
        public bool IsGrounded => GetIsGrounded();

        private void Start()
        {
            if (GetComponent<BoxCollider2D>().size != Vector2.one)
                Thrower.Throw(new InvalidOperationException());
        }

        private void OnDrawGizmos()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            Gizmos.DrawCube(transform.position, transform.lossyScale);
        }

        private bool GetIsGrounded()
        {
            var colliders = GetColliders();

            var isGrounded = false;

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var index = 0; index < colliders.Count; index++)
                if (!colliders[index].TryGetComponent<PlayerTag>(out _))
                {
                    isGrounded = true;
                    break;
                }

            return isGrounded;
        }

        private ArraySegment<Collider2D> GetColliders()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            var len = Physics2D.OverlapBoxNonAlloc(transform.position, transform.lossyScale, 0, _results,
                LayersManager.ExceptNotAGround);
            return new ArraySegment<Collider2D>(_results, 0, len);
        }
    }
}