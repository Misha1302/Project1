namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Bullet))]
    public class BulletMAnimator : MAnimator
    {
        protected void Start()
        {
            GetComponent<Bullet>().onCollision.AddListener(
                _ => Animator.SetBool(GameStaticData.Destroy, true)
            );
        }
    }
}