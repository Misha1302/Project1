namespace NamehaveCat.Scripts.Helpers
{
    using System;

    public static class Thrower
    {
        public static T Throw<T>(Exception e) => throw e;
        public static object Throw(Exception e) => Throw<object>(e);
    }
}