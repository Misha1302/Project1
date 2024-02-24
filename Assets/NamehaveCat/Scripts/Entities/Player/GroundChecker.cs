namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class GroundChecker : MonoBehaviour
    {
        [SerializeField] private float coyoteTime;

        private readonly Collider2D[] _results = new Collider2D[GameStaticData.MaxCollidersCount];

        private float _isGroundedLimitTime = 0.1f;
        public bool CanJump => GameManager.Instance.Time.CurTime < _isGroundedLimitTime;
        public bool IsGrounded { get; private set; }

        private void Start()
        {
            if (GetComponent<BoxCollider2D>().size != Vector2.one)
                Thrower.Throw(new InvalidOperationException($"Size of box collider must be {Vector2.one}"));
        }

        private void Update()
        {
            IsGrounded = GetIsGrounded();

            if (IsGrounded)
                _isGroundedLimitTime = GameManager.Instance.Time.CurTime + coyoteTime;
        }


        private void OnDrawGizmos()
        {
            // ReSharper disable once Unity.InefficientPropertyAccess
            Gizmos.DrawCube(transform.position, transform.lossyScale);
        }

        private bool GetIsGrounded()
        {
            var len = WriteColliders();
            return _results.Any(t => !t.TryGetComponent<PlayerTag>(out _), len);
        }


        private int WriteColliders() =>
            // ReSharper disable once Unity.InefficientPropertyAccess
            Physics2D.OverlapBoxNonAlloc(transform.position, transform.lossyScale, 0, _results,
                LayersManager.ExceptNotAGround);
    }
}