namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using NamehaveCat.Scripts.Health;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class EnemyHead : DamageableBase
    {
        [SerializeField] private float stunTime;
        [SerializeField] private DamageInfo damageOnStun;
        [SerializeField] private float damageWaitTime = 0.5f;

        private Enemy _enemy;
        private float _previousTime = float.MinValue;


        private void OnCollisionEnter2D(Collision2D other) => OnCollision(other.transform);
        private void OnCollisionStay2D(Collision2D other) => OnCollision(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCollision(other.transform);
        private void OnTriggerStay2D(Collider2D other) => OnCollision(other.transform);


        private void OnCollision(Component other)
        {
            if (!CanReactForCollision(other))
                return;

            _previousTime = GameManager.Instance.Time.CurTime;

            if (TryDamage())
                return;

            Stun();
        }

        private void Stun()
        {
            _enemy.WaitAndReset(
                stunTime,
                () => _enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll,
                () => _enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation
            );
        }

        private bool TryDamage()
        {
            if (_enemy.State != EnemyState.Waiting)
                return false;

            GameManager.Instance.PlayerHealth.Damage(damageOnStun);
            return true;
        }

        private bool CanReactForCollision(Component other)
        {
            if (!other.TryGetComponent<PlayerPawsTag>(out _))
                return false;

            if (_previousTime + damageWaitTime > GameManager.Instance.Time.CurTime)
                return false;

            return true;
        }

        public void Init(Enemy e)
        {
            _enemy = e;
        }
    }
}