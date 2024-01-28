namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : AnimatorBase
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            
            animator = GetComponent<Animator>();

            GameManager.Instance.InputController.onMove.AddListener(AnimateWalk);
        }

        private void Update()
        {
            animator.SetBool(AnimatorHelper.Jump, !GameManager.Instance.PlayerController.GroundChecker.IsGrounded);
        }

        private void AnimateWalk(Direction dir)
        {
            animator.SetBool(AnimatorHelper.Walk, dir.Has(Direction.Left) || dir.Has(Direction.Right));
        }
    }
}