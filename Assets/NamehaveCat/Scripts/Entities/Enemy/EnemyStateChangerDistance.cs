namespace NamehaveCat.Scripts.Entities.Enemy
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyStateChangerDistance : EnemyStateChanger
    {
        [SerializeField] private float distance = 5;
        [SerializeField] private float maxDegrees = 30;
        [SerializeField] private float offset = -0.5f;

        public override EnemyState TryGetNewState(Direction dir)
        {
            var a = transform.position + Vector3.up * offset;
            var b = GameManager.Instance.PlayerController.transform.position;

            var rightDist = Math.Abs(a.x - b.x) <= distance;
            var rightAngle = a.Degrees(b) <= maxDegrees;
            var trueDirection = dir.Has(Direction.Left) ? b.x < a.x : a.x < b.x;
            var isPlayer = RaycastPlayer(b - a);
            
            DrawLines(a, b);

            return rightDist && rightAngle && trueDirection && isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }

        private static void DrawLines(Vector3 a, Vector3 b)
        {
            Debug.DrawLine(a, a + new Vector3(4, 1), Color.red);
            Debug.DrawLine(a, a + new Vector3(4, -1), Color.red);
            Debug.DrawLine(a, a + new Vector3(-4, 1), Color.red);
            Debug.DrawLine(a, a + new Vector3(-4, -1), Color.red);
            Debug.DrawLine(a, a + new Vector3(0.1f, 0.1f), Color.red);
            Debug.DrawLine(b, b + new Vector3(0.1f, 0.1f), Color.red);
        }

        private bool RaycastPlayer(Vector2 dir)
        {
            var hit = Physics2D.Raycast(transform.position, dir, distance, LayersManager.ExceptEnemy);
            return hit != default && hit.transform.TryGetComponent<PlayerTag>(out _);
        }
    }
}