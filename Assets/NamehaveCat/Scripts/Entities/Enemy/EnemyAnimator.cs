namespace NamehaveCat.Scripts.Entities.Enemy
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : AnimatorBase
    {
        [SerializeField] private Enemy enemy;

        protected override void Start()
        {
            base.Start();

            enemy.onStateChanged.AddListener(_ =>
            {
                animator.SetBool(AnimatorHelper.Attack, enemy.State == EnemyState.Attack);
                animator.SetBool(AnimatorHelper.Walk, enemy.State == EnemyState.Walk);
                animator.SetBool(AnimatorHelper.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}