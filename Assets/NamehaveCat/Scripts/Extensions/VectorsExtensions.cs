namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using UnityEngine;

    public static class VectorsExtensions
    {
        private const float DegreesInOneRadian = (float)(180f / Math.PI);

        // toDegrees(asin(a / c))
        public static float Degrees(this Vector3 a, Vector3 b) =>
            MathF.Asin(MathF.Abs(a.y - b.y) / Vector3.Distance(a, b)) * DegreesInOneRadian;

        public static float Degrees(this Vector2 a, Vector2 b) =>
            Degrees((Vector3)a, (Vector3)b);

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