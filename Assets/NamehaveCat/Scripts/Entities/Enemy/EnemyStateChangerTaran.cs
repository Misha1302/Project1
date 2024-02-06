namespace NamehaveCat.Scripts.Entities.Enemy
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyStateChangerTaran : EnemyStateChanger
    {
        [SerializeField] private float distance = 5;
        [SerializeField] private float cooldown = 5.5f;
        [SerializeField] private float offset = -0.5f;

        public override EnemyState TryGetNewState(Direction dir)
        {
            if (GameManager.Instance.Time.CurTime < StateExitTimes[EnemyState.Attack] + cooldown)
                return EnemyState.Walk;

            Validate(dir);

            var direction = dir.Has(Direction.Left) ? Vector3.left : Vector3.right;
            var startPos = transform.position;
            startPos.y -= offset;

            // ReSharper disable Unity.InefficientPropertyAccess
            Debug.DrawLine(startPos, startPos + direction * distance, Color.red);

            var hit = Physics2D.Raycast(startPos, direction, distance, LayersManager.ExceptEnemy);
            if (hit == default)
                return EnemyState.Walk;

            var isPlayer = hit.transform.TryGetComponent<PlayerTag>(out _);
            return isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }

        private static void Validate(Direction dir)
        {
            if (!dir.Has(Direction.Left) && !dir.Has(Direction.Right))
                Thrower.Throw(new ArgumentException("Dir must be left or right"));
        }
    }
}