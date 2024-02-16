using UnityEngine;

namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Different;

    public class CheckpointAnimator : MonoBehaviour
    {
        [SerializeField] private Checkpoint checkpoint;
        [SerializeField] private Vector3 rotationSpeed;

        private void Update()
        {
            if (checkpoint.WasActivated)
                transform.Rotate(rotationSpeed * GameManager.Instance.Time.DeltaTime);
        }
    }
}