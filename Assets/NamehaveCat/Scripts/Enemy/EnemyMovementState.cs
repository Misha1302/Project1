namespace NamehaveCat.Scripts.Enemy
{
    using UnityEngine;

    public class EnemyMovementState : EnemyStateBase
    {
        [SerializeField] private float speed;

        private int _dir = 1;
        private Vector2 DirVec => Vector2.right * (_dir * speed);

        protected override void OnExit()
        {
        }

        protected override void OnEnter()
        {
        }

        public override void Loop()
        {
            var state = enemy.StateChanger.TryGetNewState(
                _dir == 1
                    ? Direction.Right
                    : Direction.Left
            );

            if (state != EnemyState.Walk)
            {
                enemy.ChangeState(state);
                return;
            }

            TryChangeDirection();

            enemy.ObjectFlipper.FlipX = _dir == 1;
            enemy.Rb2D.velocity = enemy.Rb2D.velocity.WithX(_dir * speed);
        }

        private void TryChangeDirection()
        {
            var hit = Physics2D.Raycast(transform.position, DirVec, enemy.ColliderRadius, LayerMask.GetMask("Default"));
            if (hit != default && !hit.transform.TryGetComponent<PlayerTag>(out _))
                _dir *= -1;
        }
    }
}