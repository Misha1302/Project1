namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;

    // ReSharper disable ConvertToConstant.Global
    public static class GameStaticData
    {
        public static readonly int Pressed = Animator.StringToHash("Pressed");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Unconscious = Animator.StringToHash("Unconscious");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Destroy = Animator.StringToHash("Destroy");
        public static readonly int Activation = Animator.StringToHash("Activation");
        public static readonly int NonActivated = Animator.StringToHash("NonActivated");

        public static readonly int MaxCollidersCount = 128;
        public static readonly string DefaultCoroutineName = "Nameless";
    }
}