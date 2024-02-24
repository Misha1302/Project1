namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;

    public static class ExtendedPlayerPrefs
    {
        public static void Save<T>(string key, T value) where T : struct =>
            PlayerPrefs.SetString(key, value.ToString());

        public static T Load<T>(string key, T defaultValue = default) where T : struct =>
            PlayerPrefs.HasKey(key)
                ? PlayerPrefs.GetString(key).To<T>()
                : defaultValue;
    }
}