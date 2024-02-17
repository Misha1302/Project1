namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using JetBrains.Annotations;
    using UnityEngine;

    public static class VectorsExtensions
    {
        private const float DegreesInOneRadian = (float)(180f / Math.PI);

        public static readonly Vector3 NaN3 = new(Single.NaN, Single.NaN, Single.NaN);

        // toDegrees(asin(a / c))
        [Pure] public static float Degrees(this Vector3 a, Vector3 b) =>
            MathF.Asin(MathF.Abs(a.y - b.y) / Vector3.Distance(a, b)) * DegreesInOneRadian;

        [Pure] public static float Degrees(this Vector2 a, Vector2 b) =>
            Degrees((Vector3)a, (Vector3)b);

        [Pure] public static float Distance(this Component a, Component b) =>
            Vector3.Distance(a.transform.position, b.transform.position);

        public static Vector3 MoveTo(this Component current, Component destination, float step) =>
            current.transform.position = //current.transform.position.WithX(current.transform.position.x + -step); 
                Vector3.MoveTowards(current.transform.position, destination.transform.position, Math.Abs(step));

        [Pure] public static Vector2 WithX(this Vector2 a, float x) =>
            ((Vector3)a).WithX(x);

        [Pure] public static Vector2 WithY(this Vector2 a, float y) =>
            ((Vector3)a).WithY(y);

        [Pure] public static Vector3 WithX(this Vector3 a, float x)
        {
            a.x = x;
            return a;
        }

        [Pure] public static Vector3 WithY(this Vector3 a, float y)
        {
            a.y = y;
            return a;
        }
    }
}