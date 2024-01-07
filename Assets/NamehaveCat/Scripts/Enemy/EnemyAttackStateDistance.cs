﻿namespace NamehaveCat.Scripts.Enemy
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using NamehaveCat.Scripts.LongRangeBullets;
    using UnityEngine;

    public class EnemyAttackStateDistance : EnemyStateBase
    {
        [SerializeField] private float snowballSpeed;
        [SerializeField] private float cooldown;
        [SerializeField] private Snowball snowball;

        private float _previousTime = float.NegativeInfinity;

        protected override void OnEnter()
        {
        }

        public override void Loop()
        {
            var position = transform.position;
            var playerPosition = GameManager.Instance.PlayerController.transform.position;
            var direction = (playerPosition - position).normalized * snowballSpeed;
            var right = direction.x > 0;

            var state = enemy.StateChanger.TryGetNewState(right ? Direction.Right : Direction.Left);
            if (state != EnemyState.Attack)
            {
                enemy.ChangeState(state);
                return;
            }

            if (Time.time < _previousTime + cooldown)
                return;

            _previousTime = Time.time;

            var sb = Instantiate(snowball);

            sb.Rb2D.excludeLayers = LayerMask.GetMask("Enemy");
            sb.Rb2D.position = position;
            sb.Rb2D.velocity = direction;
            sb.Flipper.FlipX = right;
            Destroy(sb.gameObject, 60);

            enemy.ObjectFlipper.FlipX = right;
        }

        protected override void OnExit()
        {
        }
    }
}