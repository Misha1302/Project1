using UnityEngine;

public static class AnimatorHelper
{
    public static readonly int Death = Animator.StringToHash("Death");
    public static readonly float DeathAnimationsTotalTime = 3f + 60f / 30f; // 3 seconds and 30 frames = equals 3.5 seconds
}