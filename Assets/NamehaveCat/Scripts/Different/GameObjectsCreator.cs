namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public static class GameObjectsCreator
    {
        public static GameObject New(string name) => new(name);

        public static T New<T>(string name) where T : Component =>
            New(name).AddComponent<T>();

        public static T1 New<T, T1>(string name) where T : Component where T1 : Component =>
            New<T>(name).gameObject.AddComponent<T1>();

        public static T2 New<T, T1, T2>(string name) where T : Component where T1 : Component where T2 : Component =>
            New<T, T1>(name).gameObject.AddComponent<T2>();
    }
}