namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Thrower
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static T Throw<T>(Exception e) => throw e;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Throw(Exception e) => Throw<object>(e);
    }
}