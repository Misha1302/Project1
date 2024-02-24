namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public static class CastsExtensions
    {
        [Pure] public static T To<T>(this string o)
        {
            var t = typeof(T);

            if (t == typeof(int)) return (T)(object)int.Parse(o);
            if (t == typeof(long)) return (T)(object)long.Parse(o);
            if (t == typeof(double)) return (T)(object)double.Parse(o);
            if (t == typeof(float)) return (T)(object)float.Parse(o);
            if (t == typeof(bool)) return (T)(object)bool.Parse(o);
            if (t == typeof(Vector3)) return (T)(object)VectorsExtensions.Parse(o);

            return Thrower.Throw<T>(new InvalidCastException("Invalid type"));
        }
    }
}