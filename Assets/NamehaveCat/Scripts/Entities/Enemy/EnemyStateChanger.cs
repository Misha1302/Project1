namespace NamehaveCat.Scripts.Entities.Enemy
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        private bool _attack;
        protected float StateAttackChangedTime { get; private set; } = float.MinValue;

        public void Init(Enemy e)
        {
            e.onStateChanged.AddListener(_ =>
            {
                // remake
                if (_attack) StateAttackChangedTime = GameManager.Instance.Time.CurTime;
                _attack = e.State == EnemyState.Attack;
            });
        }

        public abstract EnemyState TryGetNewState(Direction dir);
    }
}