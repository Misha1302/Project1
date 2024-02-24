namespace NamehaveCat.Scripts.Helpers
{
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public static class GameDynamicData
    {
        public static readonly Value<bool> ButtonsAreVisible = new(
            () => ExtendedPlayerPrefs.Load("ButtonsAreVisible", true),
            value => ExtendedPlayerPrefs.Save("ButtonsAreVisible", value)
        );

        public static readonly Value<Vector3> SpawnPosition = new(
            () => ExtendedPlayerPrefs.Load("SpawnPosition", VectorsExtensions.NaN3),
            value => ExtendedPlayerPrefs.Save("SpawnPosition", value)
        );

        public static readonly Value<float> MusicValue = new(
            () => ExtendedPlayerPrefs.Load<float>("MusicValue"),
            value => ExtendedPlayerPrefs.Save("MusicValue", value)
        );
    }
}