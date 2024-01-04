namespace NamehaveCat.Scripts.Enemy
{
    using UnityEngine;

    public class EnemyHead : MonoBehaviour
    {
        [SerializeField] private float stunTime;
        [SerializeField] private float damageOnStun;
        [SerializeField] private string deathMessage;
        private Enemy _enemy;

        private void OnCollisionEnter2D(Collision2D other) => OnCollision(other.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCollision(other.transform);

        private void OnCollision(Component other)
        {
            if (!other.TryGetComponent<PlayerPawsTag>(out _))
                return;

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