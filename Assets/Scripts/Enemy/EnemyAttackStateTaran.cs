namespace Enemy
{
    using System;
    using UnityEngine;

    public class EnemyAttackStateTaran : EnemyStateBase
    {
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;


        private int _direction;
        private bool _waiting;

        private void OnCollisionStay2D(Collision2D other)
        {
            if (_waiting) return;
            OnCollision(other);
        }

        public override void Enter()
        {
            _direction = Math.Sign(GameManager.Instance.PlayerController.transform.position.x - transform.position.x);
        }

        public override void Loop()
        {
            enemy.Rb2D.velocity = Vector2.right * (_direction * speed);
        }

        private void OnCollision(Collision2D other)
        {
            if (other.transform.TryGetComponent<GroundTag>(out _))
                return;

            var damage = GetComponent<FatalDamage>();
            enemy.WaitAndReset(cooldown, () =>
            {
                _waiting = true;
                damage.enabled = false;
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }, () =>
            {
                _waiting = false;
                damage.enabled = true;
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            });
        }
    }
}