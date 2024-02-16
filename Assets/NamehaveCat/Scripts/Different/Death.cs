namespace NamehaveCat.Scripts.Different
{
    using System.Linq;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    public static class Death
    {
        public static void Die()
        {
            var nearestCheckpoint = Object.FindObjectsOfType<Checkpoint>()
                .FirstOrDefault(x => x.WasActivated);

            GameData.startPosition = nearestCheckpoint != null
                ? nearestCheckpoint.transform.position
                : VectorsExtensions.NaN3;

            MSceneManager.Reload();
        }
    }
}