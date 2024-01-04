using System;
using UnityEngine;

namespace NamehaveCat.Scripts
{
    public static class VectorsExtensions
    {
        // toDegrees(asin(a / c))
        public static double Degrees(this Vector3 a, Vector3 b) =>
            Math.Asin(Math.Abs(a.y - b.y) / Vector3.Distance(a, b)) * (180 / Math.PI);

        public static bool Eq(this Vector3 a, Vector3 b) =>
            Math.Abs(a.x - b.x) < 0.1f && Math.Abs(a.y - b.y) < 0.1f && Math.Abs(a.z - b.z) < 0.1f;

        public static Vector2 WithX(this Vector2 a, float x)
        {
            a.x = x;
            return a;
        }
    }
}