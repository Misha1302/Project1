namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class PlayerMAnimator : MAnimator
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            GameManager.Instance.InputController.onMove.AddListener(AnimateWalk);
        }

        private void Update()
        {
            Animator.SetBool(AnimatorHelper.Jump, !GameManager.Instance.PlayerController.GroundChecker.IsGrounded);
        }

        private void AnimateWalk(Direction dir)
        {
            Animator.SetBool(AnimatorHelper.Walk, dir.Has(Direction.Left) || dir.Has(Direction.Right));
        }
    }
}