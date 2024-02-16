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
                Animator.SetBool(GameData.Attack, enemy.State == EnemyState.Attack);
                Animator.SetBool(GameData.Walk, enemy.State == EnemyState.Walk);
                Animator.SetBool(GameData.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}