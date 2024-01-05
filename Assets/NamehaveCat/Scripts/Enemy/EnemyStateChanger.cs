namespace NamehaveCat.Scripts.Enemy
{
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        protected Enemy enemy;

        public void Init(Enemy e)
        {
            enemy = e;
        }

        public abstract EnemyState TryGetNewState(Direction dir);
    }
}