namespace NamehaveCat.Scripts.Enemy
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
        [SerializeField] private EnemyStateChanger stateChanger;
        [SerializeField] private EnemyHead head;

        [CanBeNull] private EnemyStateBase _stateBeh;

        public EnemyStateChanger StateChanger => stateChanger;
        public ObjectFlipper ObjectFlipper { get; private set; }
        public Rigidbody2D Rb2D { get; private set; }
        public EnemyState State { get; private set; }

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            ObjectFlipper = GetComponent<ObjectFlipper>();

            // ReSharper disable Unity.NoNullPropagation
            attack?.Init(this);
            walk?.Init(this);
            head?.Init(this);


            ChangeState(EnemyState.Walk);
        }

        private void FixedUpdate()
        {
            _stateBeh?.Loop();
        }

        public void ChangeState(EnemyState state)
        {
            _stateBeh?.Exit();

            State = state;
            _stateBeh = state switch
            {
                EnemyState.Attack => attack,
                EnemyState.Walk => walk,
                EnemyState.Waiting => null,
                _ => throw new InvalidOperationException()
            };

            _stateBeh?.Enter();
        }

        public void WaitAndReset(float seconds, Action start, Action end)
        {
            StopAllCoroutines();
            StartCoroutine(WaitAndResetCoroutine(seconds, start, end));
        }

        private IEnumerator WaitAndResetCoroutine(float seconds, [CanBeNull] Action start, [CanBeNull] Action end)
        {
            start?.Invoke();
            ChangeState(EnemyState.Waiting);
            yield return new WaitForSeconds(seconds);
            ChangeState(EnemyState.Walk);
            end?.Invoke();
        }
    }
}