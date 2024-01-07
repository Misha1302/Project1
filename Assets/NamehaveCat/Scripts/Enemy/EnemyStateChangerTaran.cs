namespace NamehaveCat.Scripts.Enemy
{
    using System;
    using NamehaveCat.Scripts.Direction;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyStateChangerTaran : EnemyStateChanger
    {
        [SerializeField] private float distance;
        [SerializeField] private float cooldown = 5.5f;

        public override EnemyState TryGetNewState(Direction dir)
        {
            if (Time.time < StateAttackChangedTime + cooldown)
                return EnemyState.Walk;

            if (!dir.Has(Direction.Left) && !dir.Has(Direction.Right))
                throw new ArgumentException("Dir must be left or right");

            // if raycasted to player
            var direction = dir.Has(Direction.Left) ? Vector3.left : Vector3.right;
            var startPos = transform.position;
            startPos.y -= 0.5f;

            var hit = Physics2D.Raycast(startPos, direction, distance, LayerMask.GetMask("Default"));
            if (hit == default)
                return EnemyState.Walk;

            var isPlayer = hit.transform.TryGetComponent<PlayerTag>(out _);

            // ReSharper disable Unity.InefficientPropertyAccess
            Debug.DrawLine(startPos, startPos + direction * distance, Color.red);

            return isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }
    }
}