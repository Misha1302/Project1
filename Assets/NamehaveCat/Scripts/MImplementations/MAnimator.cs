namespace NamehaveCat.Scripts.MImplementations
{
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class MAnimator : MonoBehaviour
    {
        protected Animator Animator { get; private set; }

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            Animator.keepAnimatorStateOnDisable = true;
        }
    }
}