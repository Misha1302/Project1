namespace NamehaveCat.Scripts.Enemy
{
    using System;
    using UnityEngine;

    public class EnemyAttackStateTaran : EnemyStateBase
    {
        [SerializeField] private float speed;
        [SerializeField] private float cooldown;

        private int _direction;

        private Vector2 DirVec => Vector2.right * (_direction * speed);

        protected override void OnEnter()
        {
            _direction = Math.Sign(GameManager.Instance.PlayerController.transform.position.x - transform.position.x);
        }

        public override void Loop()
        {
            enemy.Rb2D.velocity = DirVec;

            ExitTaranIfNeed();
        }

        private void ExitTaranIfNeed()
        {
            var hit = Physics2D.Raycast(transform.position, DirVec, enemy.ColliderRadius, LayerMask.GetMask("Default"));
            Debug.DrawLine((Vector2)transform.position, (Vector2)transform.position + DirVec, Color.red);
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