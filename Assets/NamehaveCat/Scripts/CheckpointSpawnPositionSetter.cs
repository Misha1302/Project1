﻿namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    [RequireComponent(typeof(Checkpoint))]
    public class CheckpointSpawnPositionSetter : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Checkpoint>().activityChanged.AddListener(OnActivityChanged);
        }

        private static void OnActivityChanged(Checkpoint checkpoint)
        {
            if (checkpoint.WasActivated)
                GameData.SpawnPosition = checkpoint.transform.position;
        }
    }
}