namespace NamehaveCat.Scripts.Entities.Enemy
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class EnemyMAnimator : MAnimator
    {
        [SerializeField] private Enemy enemy;

        protected override void Start()
        {
            base.Start();

            enemy.onStateChanged.AddListener(_ =>
            {
                Animator.SetBool(AnimatorHelper.Attack, enemy.State == EnemyState.Attack);
                Animator.SetBool(AnimatorHelper.Walk, enemy.State == EnemyState.Walk);
                Animator.SetBool(AnimatorHelper.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}