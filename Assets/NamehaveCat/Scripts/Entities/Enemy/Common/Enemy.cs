namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using System;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;

    // need to be remade all enemy, but will there be an enemy in the future?
    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyStateBase attack;
        [SerializeField] private EnemyStateBase walk;
        [SerializeField] private EnemyHead head;
        [SerializeField] private float colliderRadius = 1f;
        [SerializeField] private EnemyStateChangerBase stateChanger;

        public readonly UnityEvent<Enemy> onStateChanged = new();

        private string _coroutineName;
        private EnemyStateBase _currentState;
        private EnemyStateBase _plugState;

        public EnemyStateChangerBase StateChanger => stateChanger;
        public ObjectFlipper ObjectFlipper { get; private set; }
        public Rigidbody2D Rb2D { get; private set; }
        public EnemyState State { get; private set; }

        public float ColliderRadius => colliderRadius;

        private void Start()
        {
            _coroutineName = $"WaitAndResetCoroutine{gameObject.GetInstanceID()}";
            Rb2D = GetComponent<Rigidbody2D>();
            ObjectFlipper = GetComponent<ObjectFlipper>();

            _plugState = gameObject.AddComponent<EnemyStatePlug>();

            _plugState.Init(this);
            attack.Init(this);
            walk.Init(this);
            head.Init(this);

            _currentState = _plugState;

            ChangeState(EnemyState.Walk);
        }

        private void FixedUpdate()
        {
            _currentState.Loop();
        }

        public void ChangeState(EnemyState state)
        {
            _currentState.Exit();

            State = state;
            _currentState = state switch
            {
                EnemyState.Attack => attack,
                EnemyState.Walk => walk,
                EnemyState.Waiting => _plugState,
                _ => Thrower.Throw<EnemyStateBase>(new InvalidOperationException())
            };

            _currentState.Enter();
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