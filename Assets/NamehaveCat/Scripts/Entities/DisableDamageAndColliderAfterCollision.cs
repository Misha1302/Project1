namespace NamehaveCat.Scripts.Entities
{
    using NamehaveCat.Scripts.Entities.LongRangeBullets;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(DamageableBase))]
    [RequireComponent(typeof(Collider2D))]
    public class DisableDamageAndColliderAfterCollision : MonoBehaviour
    {
        [SerializeField] private float delay = 0.1f;
        private float _time;

        private void Start()
        {
            _time = Time.time;
        }

        private void OnCollisionEnter2D(Collision2D col) => OnCol(col.transform);
        private void OnTriggerEnter2D(Collider2D other) => OnCol(other.transform);

        private void OnCol(Component tr)
        {
            if (tr.GetComponent<PlayerTag>())
                return;

            if (Time.time - _time < delay)
                return;

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