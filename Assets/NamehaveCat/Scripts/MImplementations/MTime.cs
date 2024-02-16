namespace NamehaveCat.Scripts.MImplementations
{
    using System;
    using UnityEngine;

    public class MTime : MonoBehaviour
    {
        public float CurTime { get; private set; }
        public float DeltaTime { get; private set; }

        private void FixedUpdate()
        {
            CurTime += Time.fixedDeltaTime;
        }

        private void Update()
        {
            DeltaTime = Time.deltaTime;
        }
    }
}