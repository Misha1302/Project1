﻿namespace NamehaveCat.Scripts.Enemy
{
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            enemy.onStateChanged.AddListener(_ =>
            {
                _animator.SetBool(AnimatorHelper.Attack, enemy.State == EnemyState.Attack);
                _animator.SetBool(AnimatorHelper.Walk, enemy.State == EnemyState.Walk);
                _animator.SetBool(AnimatorHelper.Unconscious, enemy.State == EnemyState.Waiting);
            });
        }
    }
}