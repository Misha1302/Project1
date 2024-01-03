namespace Enemy
{
    using System;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyStateBase attack;
        [SerializeField] private EnemyStateBase walk;
        [SerializeField] private EnemyStateChanger enemyStateChanger;

        private EnemyStateBase _curState;

        public EnemyStateChanger EnemyStateChanger => enemyStateChanger;
        public ObjectFlipper ObjectFlipper { get; private set; }
        public Rigidbody2D Rb2D { get; private set; }

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            ObjectFlipper = GetComponent<ObjectFlipper>();
            ChangeState(EnemyState.Walk);

            // ReSharper disable Unity.NoNullPropagation
            attack?.Init(this);
            walk?.Init(this);
        }

        private void FixedUpdate()
        {
            print(_curState);
            _curState?.Loop();
        }

        public void ChangeState(EnemyState state)
        {
            _curState = state switch
            {
                EnemyState.Attack => attack,
                EnemyState.Walk => walk,
                _ => throw new InvalidOperationException()
            };

            _curState.Enter();
        }

        public void WaitAndReset(float seconds, Action start, Action end)
        {
            StopAllCoroutines();
            StartCoroutine(WaitAndResetCoroutine(seconds, start, end));
        }

        private IEnumerator WaitAndResetCoroutine(float seconds, Action start, Action end)
        {
            start();
            _curState = null;
            yield return new WaitForSeconds(seconds);
            _curState = walk;
            end();
        }
    }
}