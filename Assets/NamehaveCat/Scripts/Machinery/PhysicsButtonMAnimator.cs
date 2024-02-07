namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(PhysicsButton))]
    public class PhysicsButtonMAnimator : MAnimator
    {
        private PhysicsButton _physicsButton;

        protected override void Start()
        {
            base.Start();

            _physicsButton = GetComponent<PhysicsButton>();
        }

        private void Update()
        {
            Animator.SetBool(AnimatorHelper.Pressed, _physicsButton.Pressed);
        }
    }
}