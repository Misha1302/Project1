namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(PhysicsButton))]
    public class PhysicsButtonMAnimator : MAnimator
    {
        private PhysicsButton _physicsButton;

        private void Start()
        {
            _physicsButton = GetComponent<PhysicsButton>();
        }

        private void Update()
        {
            Animator.SetBool(GameData.Pressed, _physicsButton.HasElectricity);
        }
    }
}