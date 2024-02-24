namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    public static class ColorsExtensions
    {
        [Pure] public static Color WithA(this Color color, float a)
        {
            if (a is < 0f or > 1f)
                Thrower.Throw(new ArgumentOutOfRangeException(nameof(a), "Alpha channel must be in range [0; 1]"));

            color.a = a;
            return color;
        }
    }
}