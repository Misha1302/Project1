﻿namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;

    public static class GameObjectsCreator
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static GameObject New(string name) => new(name);

        public static T New<T>(string name) where T : Component =>
            New(name).AddComponent<T>();

        public static T1 New<T, T1>(string name) where T : Component where T1 : Component =>
            New<T>(name).gameObject.AddComponent<T1>();
    }
}