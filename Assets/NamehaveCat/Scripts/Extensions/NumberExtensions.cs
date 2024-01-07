namespace NamehaveCat.Scripts.Extensions
{
    using System;

    public static class NumberExtensions
    {
        public static bool Eq(this float a, float b, float maxDelta) =>
            Math.Abs(a - b) < maxDelta;
    }
}