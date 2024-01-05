namespace NamehaveCat.Scripts.Enemy
{
    using UnityEngine;

    public class EnemyHead : MonoBehaviour
    {
        [SerializeField] private float stunTime;
        [SerializeField] private float damageOnStun;
        [SerializeField] private string deathMessage;
        [SerializeField] private float damageWaitTime = 0.5f;

        private Enemy _enemy;
        private float _previousTime;

        private void OnCollisionStay2D(Collision2D other) => OnCollision(other.transform);
        private void OnTriggerStay2D(Collider2D other) => OnCollision(other.transform);
        private void OnCollisionEnter2D(Collision2D other) => OnCollision(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCollision(other.transform);

        private void OnCollision(Component other)
        {
            print(other.name);
            if (!other.TryGetComponent<PlayerPawsTag>(out _))
                return;

            if (_previousTime + damageWaitTime > Time.time)
                return;

            _previousTime = Time.time;

            if (_enemy.State == EnemyState.Waiting)
            {
                GameManager.Instance.PlayerHealth.Damage(damageOnStun, deathMessage);
                return;
            }

            var found = TryGetComponent<FatalDamage>(out var damage);
            _enemy.WaitAndReset(stunTime, () =>
            {
                if (found) ExecuteInNextFrame.Instance.Execute(() => damage.enabled = false);
                _enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }, () =>
            {
                if (found) damage.enabled = true;
                _enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            });
        }

        public void Init(Enemy e)
        {
            _enemy = e;
        }
    }
}