namespace NamehaveCat.Scripts.Entities.Enemy
{
    using UnityEngine;

    public abstract class EnemyStateBase : MonoBehaviour
    {
        protected Enemy enemy;

        public abstract void Enter();
        public abstract void Loop();
        public abstract void Exit();

        public void Init(Enemy e) => enemy = e;
    }
}