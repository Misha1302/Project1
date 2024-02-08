namespace NamehaveCat.Scripts.Entities.Enemy.Taran
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy.Common;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyStateChangerTaran : EnemyStateChangerBase
    {
        [SerializeField] private float distance = 5;
        [SerializeField] private float offset = -0.5f;

        public override EnemyState TryGetNewState(Direction dir)
        {
            Validate(dir);

            var hit = Raycast(dir);
            if (hit == default)
                return EnemyState.Walk;

            var isPlayer = hit.transform.TryGetComponent<PlayerTag>(out _);
            return isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }

        private RaycastHit2D Raycast(Direction dir)
        {
            var direction = dir.Has(Direction.Left) ? Vector3.left : Vector3.right;
            var startPos = transform.position;
            startPos.y += offset;

            // ReSharper disable Unity.InefficientPropertyAccess
            Debug.DrawLine(startPos, startPos + direction * distance, Color.red);

            var hit = Physics2D.Raycast(startPos, direction, distance, LayersManager.ExceptEnemy);
            return hit;
        }

        private static void Validate(Direction dir)
        {
            if (!dir.Has(Direction.Left) && !dir.Has(Direction.Right))
                Thrower.Throw(new ArgumentException("Dir must be left or right"));
        }
    }
}