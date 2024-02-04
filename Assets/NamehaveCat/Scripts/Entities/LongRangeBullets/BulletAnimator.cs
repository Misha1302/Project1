namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Entities.Enemy;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletAnimator : AnimatorBase
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

            CoroutineManager.Instance.StartCoroutine(DestroyAfter5Seconds());

            IEnumerator DestroyAfter5Seconds()
            {
                yield return new MWaitForSeconds(5f);
                Destroy(gameObject);
            }
        }
    }
}