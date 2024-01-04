namespace NamehaveCat.Scripts.Enemy
{
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