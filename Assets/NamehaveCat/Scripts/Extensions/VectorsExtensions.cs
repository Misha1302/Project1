namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using UnityEngine;

    public static class VectorsExtensions
    {
        // toDegrees(asin(a / c))
        public static double Degrees(this Vector3 a, Vector3 b) =>
            Math.Asin(Math.Abs(a.y - b.y) / Vector3.Distance(a, b)) * (180 / Math.PI);

        public static Vector2 WithX(this Vector2 a, float x)
        {
            a.x = x;
            return a;
        }

        public static Vector2 WithY(this Vector2 a, float y)
        {
            a.y = y;
            return a;
        }
    }
}