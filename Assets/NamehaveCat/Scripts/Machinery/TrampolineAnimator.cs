﻿namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class TrampolineAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play()
        {
            _animator.SetTrigger(AnimatorHelper.Jump);
        }
    }
}