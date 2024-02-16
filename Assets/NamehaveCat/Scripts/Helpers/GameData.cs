﻿namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using Cinemachine.Utility;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    // ReSharper disable ConvertToConstant.Global
    public static class GameData
    {
        public static readonly int Pressed = Animator.StringToHash("Pressed");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Unconscious = Animator.StringToHash("Unconscious");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Destroy = Animator.StringToHash("Destroy");

        public static readonly int MaxCollidersCount = 128;
        
        public static Vector3 startPosition = VectorsExtensions.NaN3;
    }
}