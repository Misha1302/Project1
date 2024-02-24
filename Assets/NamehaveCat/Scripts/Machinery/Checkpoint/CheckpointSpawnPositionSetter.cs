namespace NamehaveCat.Scripts.Machinery.Checkpoint
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
                GameDynamicData.SpawnPosition.set(checkpoint.transform.position);
        }
    }
}