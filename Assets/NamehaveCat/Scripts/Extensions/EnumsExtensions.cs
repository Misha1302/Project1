namespace NamehaveCat.Scripts.Extensions
{
    using System;
    using JetBrains.Annotations;

    public static class EnumsExtensions
    {
        [Pure] public static bool Has<T>(this T type, T value) where T : Enum =>
            (type.AsI32() & value.AsI32()) == value.AsI32();

        [Pure] public static bool Is<T>(this T type, T value) where T : Enum =>
            type.AsI32() == value.AsI32();

        [Pure] public static T Add<T>(this T type, T value) where T : Enum =>
            (type.AsI32() | value.AsI32()).AsEnum<T>();

        [Pure] public static T Remove<T>(this Enum type, T value) where T : Enum =>
            (type.AsI32() & ~value.AsI32()).AsEnum<T>();


        private static int AsI32<T>(this T enumValue) where T : Enum => (int)(object)enumValue;

        private static T AsEnum<T>(this int enumValue) where T : Enum => (T)(object)enumValue;
    }
}