namespace NamehaveCat.Scripts.Enemy
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    public class EnemyAttackStateDistance : EnemyStateBase
    {
        [SerializeField] private float snowballSpeed;
        [SerializeField] private float cooldown;
        [SerializeField] private Rigidbody2D snowball;

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

            sb.excludeLayers = LayerMask.GetMask("Enemy");
            sb.position = position;
            sb.velocity = direction;
            sb.GetComponent<ObjectFlipper>().FlipX = right;
            Destroy(sb, 60);

            enemy.ObjectFlipper.FlipX = right;
        }

        protected override void OnExit()
        {
        }
    }
}