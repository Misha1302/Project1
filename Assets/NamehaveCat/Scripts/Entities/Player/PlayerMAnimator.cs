namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class PlayerMAnimator : MAnimator
    {
        private void Start()
        {
            GameManager.Instance.InputController.onPress.AddListener(AnimateWalk);
        }

        private void Update()
        {
            Animator.SetBool(GameConstants.Jump, StateManager.IsFlying);
        }

        private void AnimateWalk(Direction dir)
        {
            Animator.SetBool(GameConstants.Walk, dir.Has(Direction.Left) ^ dir.Has(Direction.Right));
        }
    }
}