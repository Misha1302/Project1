namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using NamehaveCat.Scripts.Different;
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
                Animator.SetBool(AnimatorHelper.Attack, enemy.State == EnemyState.Attack);
                Animator.SetBool(AnimatorHelper.Walk, enemy.State == EnemyState.Walk);
                Animator.SetBool(AnimatorHelper.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}