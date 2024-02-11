namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;

    public static class AnimatorHelper
    {
        public static readonly int Pressed = Animator.StringToHash("Pressed");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Unconscious = Animator.StringToHash("Unconscious");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Destroy = Animator.StringToHash("Destroy");
    }
}