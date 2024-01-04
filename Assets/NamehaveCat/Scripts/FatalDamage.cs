namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class FatalDamage : MonoBehaviour
    {
        [SerializeField] private string message;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Damage(other.transform);
        }

        public void Damage(Transform t)
        {
            if (!enabled) return;

            if (t.TryGetComponent<Health>(out var health))
                health.Damage(float.MaxValue, message);
        }
    }
}