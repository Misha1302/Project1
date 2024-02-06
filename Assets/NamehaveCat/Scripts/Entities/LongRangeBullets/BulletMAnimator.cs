namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMAnimator : MAnimator
    {
        private Animator _animator;
        private Rigidbody2D _rb2D;

        protected override void Start()
        {
            base.Start();

            _rb2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void DestroyBullet()
        {
            _animator.SetBool(AnimatorHelper.Destroy, true);

            if (TryGetComponent<Collider2D>(out var col))
                col.enabled = false;

            _rb2D.velocity = Vector2.zero;
            _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;

            CoroutineManager.Instance.InvokeAfter(() => Destroy(gameObject), 5);
        }
    }
}