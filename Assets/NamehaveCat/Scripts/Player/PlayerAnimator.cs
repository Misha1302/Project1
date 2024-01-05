namespace NamehaveCat.Scripts.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;

        // Start is called before the first frame update
        private void Start()
        {
            _animator = GetComponent<Animator>();

            GameManager.Instance.InputController.onMove.AddListener(AnimateWalk);
        }

        private void Update()
        {
            _animator.SetBool(AnimatorHelper.Jump, !GameManager.Instance.PlayerController.GroundChecker.IsGrounded);
        }

        private void AnimateWalk(Direction dir)
        {
            _animator.SetBool(AnimatorHelper.Walk, dir.Has(Direction.Left) || dir.Has(Direction.Right));
        }
    }
}