namespace NamehaveCat.Scripts.Machinery.Checkpoint
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

        private void OnActivityChanged(Checkpoint _)
        {
            Animator.SetTrigger(checkpoint.WasActivated ? GameStaticData.Activation : GameStaticData.NonActivated);
        }
    }
}