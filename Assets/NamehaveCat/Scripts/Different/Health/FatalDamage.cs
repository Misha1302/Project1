namespace NamehaveCat.Scripts.Different.Health
{
    using UnityEngine;

    public class FatalDamage : DamageableBase
    {
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other) => Damage(other.transform);
        private void OnCollisionStay2D(Collision2D other) => Damage(other.transform);

        private void OnTriggerEnter2D(Collider2D other) => Damage(other.transform);
        private void OnTriggerStay2D(Collider2D other) => Damage(other.transform);

        public void Damage(Transform t)
        {
            if (!enabled)
                return;

            var health = t.GetComponentInParent<Health>();
            if (health != null)
                health.Damage(float.MaxValue, message);
        }
    }
}