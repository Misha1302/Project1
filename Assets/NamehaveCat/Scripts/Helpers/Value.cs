namespace NamehaveCat.Scripts.Helpers
{
    using System;
    using UnityEngine.Events;

    public class Value<T>
    {
        public readonly Func<T> get;
        public readonly Action<T> set;
        public readonly UnityEvent<T> onSet = new();

        public Value(Func<T> get, Action<T> set)
        {
            this.get = get;
            this.set = set + onSet.Invoke;
        }
    }
}