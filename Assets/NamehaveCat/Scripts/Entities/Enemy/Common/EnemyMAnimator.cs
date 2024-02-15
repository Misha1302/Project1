namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class EnemyMAnimator : MAnimator
    {
        [SerializeField] private Enemy enemy;

        protected override void Awake()
        {
            base.Awake();

            enemy.onStateChanged.AddListener(_ =>
            {
                Animator.SetBool(GameConstants.Attack, enemy.State == EnemyState.Attack);
                Animator.SetBool(GameConstants.Walk, enemy.State == EnemyState.Walk);
                Animator.SetBool(GameConstants.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}