namespace Enemy
{
    using System;
    using UnityEngine;

    public class EnemyMovementState : EnemyStateBase
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        private Transform _dest;
        private int _direction;


        private void Start()
        {
            _dest = point2;
        }

        public override void Enter()
        {
        }

        public override void Loop()
        {
            var state = enemy.EnemyStateChanger.TryGetNewState(_direction == 1 ? Direction.Right : Direction.Left);
            if (state != EnemyState.Walk)
            {
                enemy.ChangeState(state);
                return;
            }

            _direction = Math.Sign(_dest.position.x - transform.position.x);

            enemy.ObjectFlipper.FlipX = _direction == 1;
            print(enemy.Rb2D.velocity);
            enemy.Rb2D.velocity = enemy.Rb2D.velocity.WithX(_direction * speed);

            ChangeDest();
        }

        private void ChangeDest()
        {
            var enX = transform.position.x;
            var p1X = point1.position.x;
            var p2X = point2.position.x;

            // p1X < enX < p2X - if between two points
            if (p1X < enX && enX < p2X)
                return;

            if (enX <= p1X) _dest = point2;
            else if (enX >= p2X) _dest = point1;
            else throw new InvalidOperationException();
        }
    }
}