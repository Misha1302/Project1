namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rb2D;

        private void Start()
        {
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

            Destroy(gameObject, 5f); // with margin
        }
    }
}