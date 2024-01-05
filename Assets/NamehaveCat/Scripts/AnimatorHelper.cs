namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public static class AnimatorHelper
    {
        // ReSharper disable once ConvertToConstant.Global
        // 3 seconds and 30 frames = equals 3.5 seconds
        public static readonly float DeathAnimationsTotalTime = 3f + 60f / 30f;

        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Unconscious = Animator.StringToHash("Unconscious");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Destroy = Animator.StringToHash("Destroy");
    }
}