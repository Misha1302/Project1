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

        private void OnCollision(Component other)
        {
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

            _enemy.WaitAndReset(stunTime, null, null);
        }

        public void Init(Enemy e)
        {
            _enemy = e;
        }
    }
}