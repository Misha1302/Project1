namespace NamehaveCat.Scripts.Different
{
    using System;
    using UnityEngine;

    public class FatalDamage : MonoBehaviour
    {
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other) => Damage(other.transform);
        private void OnCollisionStay2D(Collision2D other) => Damage(other.transform);

        public void Damage(Transform t)
        {
            if (!enabled) return;
            var health = t.GetComponentInParent<Health>();
            if (health != null)
                health.Damage(float.MaxValue, message);
        }
    }
}