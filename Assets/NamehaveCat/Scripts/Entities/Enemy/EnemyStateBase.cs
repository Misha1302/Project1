namespace NamehaveCat.Scripts.Entities.Enemy
{
    using UnityEngine;

    public abstract class EnemyStateBase : MonoBehaviour
    {
        protected Enemy enemy;
        private bool _enter;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_enter || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                return;

            OnColEnter(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                return;

            OnColStay(other);
        }

        public void Enter()
        {
            _enter = true;
            OnEnter();
        }

        protected abstract void OnEnter();

        public abstract void Loop();

        public void Exit()
        {
            _enter = false;
            OnExit();
        }

        protected abstract void OnExit();


        public void Init(Enemy e)
        {
            enemy = e;
        }

        protected virtual void OnColEnter(Collision2D collision2D)
        {
        }

        protected virtual void OnColStay(Collision2D other)
        {
        }
    }
}