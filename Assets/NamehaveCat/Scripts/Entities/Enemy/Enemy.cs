namespace NamehaveCat.Scripts.Entities.Enemy
{
    using System;
    using System.Collections;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private EnemyStateBase attack;
        [SerializeField] [CanBeNull] private EnemyStateBase walk;
        [SerializeField] private EnemyHead head;
        [SerializeField] private float colliderRadius = 1f;
        [SerializeField] [CanBeNull] private EnemyStateChangerBase stateChanger;

        [HideInInspector] public UnityEvent<Enemy> onStateChanged = new();
        private readonly string _coroutineName = $"WaitAndResetCoroutine{Guid.NewGuid()}";
        [CanBeNull] private EnemyStateBase _stateBeh;


        public EnemyStateChangerBase StateChanger => stateChanger;
        public ObjectFlipper ObjectFlipper { get; private set; }
        public Rigidbody2D Rb2D { get; private set; }
        public EnemyState State { get; private set; }

        public float ColliderRadius => colliderRadius;

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            ObjectFlipper = GetComponent<ObjectFlipper>();

            if (attack != null) attack.Init(this);
            if (walk != null) walk.Init(this);
            if (head != null) head.Init(this);

            GameManager.Instance.ExecutorInNextFrame.Execute(() => ChangeState(EnemyState.Walk));
        }

        private void FixedUpdate()
        {
            if (_stateBeh != null) _stateBeh.Loop();
        }

        public void ChangeState(EnemyState state)
        {
            if (_stateBeh != null) _stateBeh.Exit();

            State = state;
            _stateBeh = state switch
            {
                EnemyState.Attack => attack,
                EnemyState.Walk => walk,
                EnemyState.Waiting => null,
                _ => Thrower.Throw<EnemyStateBase>(new InvalidOperationException())
            };

            if (_stateBeh != null) _stateBeh.Enter();
            onStateChanged?.Invoke(this);
        }

        public void WaitAndReset(float seconds, Action start, Action end)
        {
            GameManager.Instance.CoroutineManager.StopCoroutines(_coroutineName);

            GameManager.Instance.CoroutineManager.StartCoroutine(
                WaitAndResetCoroutine(seconds, start, end),
                _coroutineName
            );
        }

        private IEnumerator WaitAndResetCoroutine(float seconds, [CanBeNull] Action start, [CanBeNull] Action end)
        {
            start?.Invoke();
            ChangeState(EnemyState.Waiting);

            yield return new MWaitForSeconds(seconds);

            ChangeState(EnemyState.Walk);
            end?.Invoke();
        }
    }
}