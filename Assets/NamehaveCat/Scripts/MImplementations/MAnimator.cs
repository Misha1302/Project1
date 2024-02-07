﻿namespace NamehaveCat.Scripts.MImplementations
{
    using UnityEngine;

    public class MAnimator : MonoBehaviour
    {
        protected Animator Animator { get; private set; }

        protected virtual void Start()
        {
            Animator = GetComponent<Animator>();
            Animator.keepAnimatorStateOnDisable = true;
        }
    }
}