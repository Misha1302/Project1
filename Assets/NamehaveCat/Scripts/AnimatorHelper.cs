using UnityEngine;

namespace NamehaveCat.Scripts
{
    // ReSharper disable ConvertToConstant.Global
    public static class AnimatorHelper
    {
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly float DeathAnimationsTotalTime = 3f + 60f / 30f; // 3 seconds and 30 frames = equals 3.5 seconds
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Jump = Animator.StringToHash("Jump");
    }
}