namespace NamehaveCat.Scripts.Entities.Enemy.Distance
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy.Common;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyStateChangerDistance : EnemyStateChangerBase
    {
        [SerializeField] private float distance = 5;
        [SerializeField] private float maxDegrees = 30;
        [SerializeField] private float offset = -0.5f;

        public override EnemyState TryGetNewState(Direction dir)
        {
            var a = transform.position + Vector3.up * offset;
            var b = GameManager.Instance.PlayerController.transform.position;

            var trueDist = Math.Abs(a.x - b.x) <= distance;
            var trueAngle = a.Degrees(b) <= maxDegrees;
            var trueDirection = dir.Has(Direction.Left) ? b.x < a.x : a.x < b.x;
            var isPlayer = RaycastPlayer(b - a);

            DrawLines(a, dir.Has(Direction.Left));

            return trueDist && trueAngle && trueDirection && isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }

        private static void DrawLines(Vector3 enemyCenterPoint, bool left)
        {
            if (left)
            {
                Debug.DrawLine(enemyCenterPoint, enemyCenterPoint + new Vector3(-4, 1), Color.red);
                Debug.DrawLine(enemyCenterPoint, enemyCenterPoint + new Vector3(-4, -1), Color.red);
            }
            else
            {
                Debug.DrawLine(enemyCenterPoint, enemyCenterPoint + new Vector3(4, 1), Color.red);
                Debug.DrawLine(enemyCenterPoint, enemyCenterPoint + new Vector3(4, -1), Color.red);
            }
        }

        private bool RaycastPlayer(Vector2 dir)
        {
            var hit = Physics2D.Raycast(transform.position, dir, distance, LayersManager.ExceptEnemy);
            return hit != default && hit.transform.TryGetComponent<PlayerTag>(out _);
        }
    }
}