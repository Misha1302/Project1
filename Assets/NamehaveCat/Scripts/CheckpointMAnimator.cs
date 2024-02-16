namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    public class CheckpointMAnimator : MAnimator
    {
        [SerializeField] private Checkpoint checkpoint;

        private void Start()
        {
            checkpoint.activityChanged.AddListener(OnActivityChanged);
        }

        private void OnActivityChanged(Checkpoint unused)
        {
            Animator.SetTrigger(checkpoint.WasActivated ? GameData.Activation : GameData.NonActivated);
        }
    }
}