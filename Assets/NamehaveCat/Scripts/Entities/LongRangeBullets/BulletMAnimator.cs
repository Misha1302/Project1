namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMAnimator : MAnimator
    {
        private Rigidbody2D _rb2D;

        protected void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        public void DestroyBullet()
        {
            Animator.SetBool(AnimatorHelper.Destroy, true);

            if (TryGetComponent<Collider2D>(out var col))
                col.enabled = false;

            _rb2D.velocity = Vector2.zero;
            _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;

            GameManager.Instance.CoroutineManager.InvokeAfter(() => Destroy(gameObject), 5);
        }
    }
}