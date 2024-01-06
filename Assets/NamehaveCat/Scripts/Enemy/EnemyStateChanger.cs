namespace NamehaveCat.Scripts.Enemy
{
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        protected float StateChangeTime { get; private set; } = float.MinValue;

        public void Init(Enemy e)
        {
            e.onStateChanged.AddListener(_ => StateChangeTime = Time.time);
        }

        public abstract EnemyState TryGetNewState(Direction dir);
    }
}