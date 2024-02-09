namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyMovementState : EnemyStateBase
    {
        [SerializeField] private float speed;

        private int _dir = 1;

        private Vector2 DirVec => Vector2.right * (_dir * speed);

        public override void Exit()
        {
        }

        public override void Enter()
        {
        }

        public override void Loop()
        {
            if (TryChangeState())
                return;

            ChangeDirectionIfNeed();

            SetFlipAndVelocity();
        }

        private void SetFlipAndVelocity()
        {
            enemy.ObjectFlipper.FlipX = _dir == 1;
            enemy.Rb2D.velocity = enemy.Rb2D.velocity.WithX(_dir * speed);
        }

        private bool TryChangeState()
        {
            var state = enemy.StateChanger.TryGetNewState(
                _dir == 1
                    ? Direction.Right
                    : Direction.Left
            );

            if (state != EnemyState.Walk)
            {
                enemy.ChangeState(state);
                return true;
            }

            return false;
        }

        private void ChangeDirectionIfNeed()
        {
            var hit = Physics2D.Raycast(transform.position, DirVec, enemy.ColliderRadius, LayersManager.ExceptEnemy);
            if (hit != default && !hit.transform.TryGetComponent<PlayerTag>(out _))
                _dir *= -1;
        }
    }
}