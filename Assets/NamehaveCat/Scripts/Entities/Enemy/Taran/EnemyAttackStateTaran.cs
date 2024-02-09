namespace NamehaveCat.Scripts.Entities.Enemy.Taran
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy.Common;
    using NamehaveCat.Scripts.Health;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(FatalDamage))]
    public class EnemyAttackStateTaran : EnemyStateBase
    {
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;
        [SerializeField] private float startTime;

        private int _direction;
        private float _time;

        private Vector2 DirVec => Vector2.right * (_direction * speed);

        private bool CanMove => GameManager.Instance.Time.CurTime > _time + startTime;

        public override void Enter()
        {
            _direction = Math.Sign(GameManager.Instance.PlayerController.transform.position.x - transform.position.x);
            _time = GameManager.Instance.Time.CurTime;
        }

        public override void Loop()
        {
            enemy.Rb2D.velocity = CanMove ? DirVec : Vector2.zero;

            ExitTaranIfNeed();
        }

        private void ExitTaranIfNeed()
        {
            var position = transform.position;

            var hit = Physics2D.Raycast(position, DirVec, enemy.ColliderRadius, LayersManager.ExceptEnemy);
            Debug.DrawLine(position, position + (Vector3)DirVec, Color.red);

            if (hit == default)
                return;

            if (hit.transform.TryGetComponent<PlayerTag>(out _))
                GetComponent<FatalDamage>().Damage(hit.transform);

            ExitTaran();
        }

        private void ExitTaran()
        {
            var damage = GetComponent<FatalDamage>();

            enemy.WaitAndReset(
                cooldown,
                () =>
                {
                    damage.enabled = false;
                    enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
                },
                () =>
                {
                    damage.enabled = true;
                    enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            );
        }

        public override void Exit()
        {
        }
    }
}