namespace NamehaveCat.Scripts.Enemy
{
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        private bool _attack;
        protected float StateAttackChangedTime { get; private set; }

        public void Init(Enemy e)
        {
            e.onStateChanged.AddListener(_ =>
            {
                if (_attack) StateAttackChangedTime = Time.time;
                _attack = e.State == EnemyState.Attack;
            });
        }

        public abstract EnemyState TryGetNewState(Direction dir);
    }
}