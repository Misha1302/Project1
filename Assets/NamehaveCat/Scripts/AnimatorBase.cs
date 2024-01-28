namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class AnimatorBase : MonoBehaviour
    {
        protected Animator animator;

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            animator.keepAnimatorStateOnDisable = true;
        }
    }
}