namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using System;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
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
        private readonly string _coroutineName = $"WaitAndResetCoroutine{Guid.NewGuid()}";

        public readonly UnityEvent<Enemy> onStateChanged = new();
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

            ChangeState(EnemyState.Walk);
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
            onStateChanged.Invoke(this);
        }

        public void WaitAndReset(float seconds, [CanBeNull] Action start, [CanBeNull] Action end)
        {
            GameManager.Instance.CoroutineManager.StopCoroutines(_coroutineName);

            start?.Invoke();
            ChangeState(EnemyState.Waiting);

            GameManager.Instance.CoroutineManager.InvokeAfter(
                () =>
                {
                    ChangeState(EnemyState.Walk);
                    end?.Invoke();
                },
                seconds,
                _coroutineName
            );
        }
    }
}