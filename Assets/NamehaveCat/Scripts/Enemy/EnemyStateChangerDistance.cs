namespace NamehaveCat.Scripts.Enemy
{
    using System;
    using UnityEngine;

    public class EnemyStateChangerDistance : EnemyStateChanger
    {
        [SerializeField] private float distance = 5;
        [SerializeField] private float maxDegrees = 30;

        public override EnemyState TryGetNewState(Direction dir)
        {
            var a = transform.position;
            var b = GameManager.Instance.PlayerController.transform.position;

            var rightDist = Math.Abs(a.x - b.x) <= distance;
            var rightAngle = a.Degrees(b) <= maxDegrees;
            var rightDirection = dir.Has(Direction.Left) ? b.x < a.x : a.x < b.x;
            var isPlayer = RaycastPlayer(b - a);

            return rightDist && rightAngle && rightDirection && isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }

        private bool RaycastPlayer(Vector2 dir)
        {
            var hit = Physics2D.Raycast(transform.position, dir, distance, LayerMask.GetMask("Default"));
            return hit != default && hit.transform.TryGetComponent<PlayerTag>(out _);
        }
    }
}