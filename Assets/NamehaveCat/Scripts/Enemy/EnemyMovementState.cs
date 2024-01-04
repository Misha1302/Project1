namespace NamehaveCat.Scripts.Enemy
{
    using UnityEngine;

    public class EnemyMovementState : EnemyStateBase
    {
        [SerializeField] private float speed;
        private int _dirInternal = -1;

        private int Dir
        {
            get => _dirInternal;
            set
            {
                _dirInternal = value;
                enemy.ObjectFlipper.FlipX = _dirInternal == 1;
            }
        }

        protected override void OnColEnter(Collision2D collision2D)
        {
            if (collision2D.transform.TryGetComponent<PlayerTag>(out _))
                return;

            Dir *= -1;
        }

        protected override void OnExit()
        {
        }

        protected override void OnEnter()
        {
            Dir *= -1;
        }

        public override void Loop()
        {
            var state = enemy.StateChanger.TryGetNewState(
                Dir == 1
                    ? Direction.Right
                    : Direction.Left
            );

            if (state != EnemyState.Walk)
            {
                enemy.ChangeState(state);
                return;
            }

            enemy.Rb2D.velocity = enemy.Rb2D.velocity.WithX(Dir * speed);
        }
    }
}