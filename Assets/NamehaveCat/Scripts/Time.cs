namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class MTime : MonoBehaviour
    {
        public float CurTime { get; private set; }

        private void Update()
        {
            CurTime += Time.deltaTime;
        }
    }
}