namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public static class AnimatorHelper
    {
        // ReSharper disable ConvertToConstant.Local
        private static readonly float _waitTime = 1f + 30f / 60f;

        // 3 seconds and 30 frames = equals 3.5 seconds
        public static readonly float DeathAnimationsTotalTime = 3f + 30f / 60f + _waitTime;

        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Unconscious = Animator.StringToHash("Unconscious");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Destroy = Animator.StringToHash("Destroy");
    }
}