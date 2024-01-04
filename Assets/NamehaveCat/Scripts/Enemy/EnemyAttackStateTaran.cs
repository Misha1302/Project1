namespace NamehaveCat.Scripts.Enemy
{
    using System;
    using UnityEngine;

    public class EnemyAttackStateTaran : EnemyStateBase
    {
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;

        private int _direction;

        protected override void OnColEnter(Collision2D other)
        {
            OnCollision(other);
        }

        protected override void OnEnter()
        {
            _direction = Math.Sign(GameManager.Instance.PlayerController.transform.position.x - transform.position.x);
        }

        public override void Loop()
        {
            enemy.Rb2D.velocity = Vector2.right * (_direction * speed);
        }

        protected override void OnExit()
        {
        }

        private void OnCollision(Collision2D other)
        {
            if (other.transform.TryGetComponent<GroundTag>(out _))
                return;

            var damage = GetComponent<FatalDamage>();
            enemy.WaitAndReset(cooldown, () =>
            {
                ExecuteInNextFrame.Instance.Execute(() => damage.enabled = false);
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
                print(damage.enabled);
            }, () =>
            {
                damage.enabled = true;
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                print(damage.enabled);
            });
        }
    }
}