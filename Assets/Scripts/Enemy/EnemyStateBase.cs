namespace Enemy
{
    using UnityEngine;

    public abstract class EnemyStateBase : MonoBehaviour
    {
        protected Enemy enemy;

        public abstract void Enter();
        public abstract void Loop();

        public void Init(Enemy e)
        {
            enemy = e;
        }
    }
}