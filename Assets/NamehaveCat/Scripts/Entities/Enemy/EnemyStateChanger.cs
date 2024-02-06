namespace NamehaveCat.Scripts.Entities.Enemy
{
    using System.Collections.Generic;
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        private readonly Dictionary<EnemyState, float> _stateExitTimes = new();
        private EnemyState _prevState = EnemyState.Invalid;

        protected IReadOnlyDictionary<EnemyState, float> StateExitTimes => _stateExitTimes;

        private void Start()
        {
            _stateExitTimes.Add(EnemyState.Attack, float.MinValue);
            _stateExitTimes.Add(EnemyState.Waiting, float.MinValue);
            _stateExitTimes.Add(EnemyState.Walk, float.MinValue);
        }

        public void Init(Enemy e)
        {
            e.onStateChanged.AddListener(_ =>
            {
                _stateExitTimes[_prevState] = GameManager.Instance.Time.CurTime;
                _prevState = e.State;
            });
        }

        public abstract EnemyState TryGetNewState(Direction dir);
    }
}