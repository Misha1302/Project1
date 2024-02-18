namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Health;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(DamageableBase))]
    [RequireComponent(typeof(Collider2D))]
    public class DamageAndColliderAfterCollisionDisabler : MonoBehaviour
    {
        [SerializeField] private float delay = 0.1f;
        private float _time;

        private void OnEnable()
        {
            _time = GameManager.Instance.Time.CurTime;
        }

        private void OnCollisionEnter2D(Collision2D col) => OnCol(col.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCol(other.transform);

        private void OnCol(Component tr)
        {
            if (Validator.ValidateEnemyCollision(tr) && !TooEarly())
                DisableComponents();
        }

        private bool TooEarly() => GameManager.Instance.Time.CurTime - _time < delay;

        private void DisableComponents()
        {
            foreach (var c in GetComponentsInChildren<DamageableBase>())
                c.enabled = false;
            foreach (var c in GetComponentsInChildren<Collider2D>())
                c.enabled = false;
            foreach (var c in GetComponentsInChildren<Rigidbody2D>())
                c.simulated = false;

            enabled = false;
        }
    }
}