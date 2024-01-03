namespace Enemy
{
    using System;
    using System.Collections;
    using JetBrains.Annotations;
    using UnityEngine;

    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyStateBase attack;
        [SerializeField] private EnemyStateBase walk;
        [SerializeField] private EnemyStateChanger enemyStateChanger;

        [CanBeNull] private EnemyStateBase _curState;

        public EnemyStateChanger EnemyStateChanger => enemyStateChanger;
        public ObjectFlipper ObjectFlipper { get; private set; }
        public Rigidbody2D Rb2D { get; private set; }

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            ObjectFlipper = GetComponent<ObjectFlipper>();

            // ReSharper disable Unity.NoNullPropagation
            attack?.Init(this);
            walk?.Init(this);


            ChangeState(EnemyState.Walk);
        }

        private void FixedUpdate()
        {
            _curState?.Loop();
        }

        public void ChangeState(EnemyState state)
        {
            _curState?.Exit();

            _curState = state switch
            {
                EnemyState.Attack => attack,
                EnemyState.Walk => walk,
                EnemyState.Waiting => null,
                _ => throw new InvalidOperationException()
            };

            _curState?.Enter();
        }

        public void WaitAndReset(float seconds, Action start, Action end)
        {
            StopAllCoroutines();
            StartCoroutine(WaitAndResetCoroutine(seconds, start, end));
        }

        private IEnumerator WaitAndResetCoroutine(float seconds, Action start, Action end)
        {
            start();
            ChangeState(EnemyState.Waiting);
            yield return new WaitForSeconds(seconds);
            ChangeState(EnemyState.Walk);
            end();
        }
    }
}