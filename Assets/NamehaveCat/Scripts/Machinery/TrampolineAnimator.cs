namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Trampoline))]
    public class TrampolineAnimator : MAnimator
    {
        private void Start()
        {
            GetComponent<Trampoline>().onCollision.AddListener(_ => Animator.SetTrigger(GameStaticData.Jump));
        }
    }
}