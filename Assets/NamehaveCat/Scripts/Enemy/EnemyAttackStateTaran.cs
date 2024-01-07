namespace NamehaveCat.Scripts.Enemy
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Tags;
    using NamehaveCat.Scripts.Velocipedi;
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

        protected override void OnEnter()
        {
            _direction = Math.Sign(GameManager.Instance.PlayerController.transform.position.x - transform.position.x);
            _time = Time.time;
        }

        public override void Loop()
        {
            enemy.Rb2D.velocity = Time.time > _time + startTime ? DirVec : Vector2.zero;

            ExitTaranIfNeed();
        }

        private void ExitTaranIfNeed()
        {
            var position = transform.position;

            var hit = Physics2D.Raycast(position, DirVec, enemy.ColliderRadius, LayerMask.GetMask("Default"));
            Debug.DrawLine((Vector2)position, (Vector2)position + DirVec, Color.red);
            if (hit != default)
            {
                if (hit.transform.TryGetComponent<PlayerTag>(out _))
                    GetComponent<FatalDamage>().Damage(hit.transform);

                ExitTaran();
            }
        }

        private void ExitTaran()
        {
            var damage = GetComponent<FatalDamage>();
            enemy.WaitAndReset(cooldown, () =>
            {
                ExecuteInNextFrame.Instance.Execute(() => damage.enabled = false);
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }, () =>
            {
                damage.enabled = true;
                enemy.Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            });
        }

        protected override void OnExit()
        {
        }
    }
}