namespace NamehaveCat.Scripts.MImplementations
{
    using UnityEngine;

    public class MTime : MonoBehaviour
    {
        public float CurTime { get; private set; }
        public float DeltaTime { get; private set; }

        private void Update()
        {
            DeltaTime = Time.deltaTime;
        }

        private void FixedUpdate()
        {
            CurTime += Time.fixedDeltaTime;
        }
    }
}