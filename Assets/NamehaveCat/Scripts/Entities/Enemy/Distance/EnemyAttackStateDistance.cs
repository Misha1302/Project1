namespace NamehaveCat.Scripts.Entities.Enemy.Distance
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy.Common;
    using NamehaveCat.Scripts.Entities.LongRangeBullets;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    public class EnemyAttackStateDistance : EnemyStateBase
    {
        [SerializeField] private float snowballSpeed;
        [SerializeField] private float cooldown;
        [SerializeField] private Snowball snowball;

        private float _previousTime = float.NegativeInfinity;

        public override void Enter()
        {
        }

        public override void Loop()
        {
            var direction = CalcDirection();

            if (TryChangeState(direction))
                return;

            if (!TryThrowSnowball(direction))
                return;

            RotateEnemy(direction);
        }

        private void RotateEnemy(Vector3 direction)
        {
            enemy.ObjectFlipper.FlipX = direction.x > 0;
        }

        private bool TryThrowSnowball(Vector3 direction)
        {
            if (GameManager.Instance.Time.CurTime < _previousTime + cooldown)
                return false;

            _previousTime = GameManager.Instance.Time.CurTime;

            ThrowSnowball(direction);

            return true;
        }

        private void ThrowSnowball(Vector3 direction)
        {
            var sb = Instantiate(snowball);

            sb.Set(LayersManager.Enemy, transform.position, direction);

            GameManager.Instance.CoroutineManager.InvokeAfter(() =>
            {
                if (sb != null)
                    Destroy(sb.gameObject);
            }, 60);
        }

        private bool TryChangeState(Vector3 direction)
        {
            var state = enemy.StateChanger.TryGetNewState(direction.x > 0 ? Direction.Right : Direction.Left);
            if (state == EnemyState.Attack)
                return false;

            enemy.ChangeState(state);
            return true;
        }

        private Vector3 CalcDirection()
        {
            var position = transform.position;
            var playerPosition = GameManager.Instance.PlayerController.transform.position;
            var direction = (playerPosition - position).normalized * snowballSpeed;
            return direction;
        }

        public override void Exit()
        {
        }
    }
}