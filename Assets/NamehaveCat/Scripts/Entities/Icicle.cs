namespace NamehaveCat.Scripts.Entities
{
    using System.Collections;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class Icicle : MonoBehaviour
    {
        [SerializeField] private Vector3 startVel;
        [SerializeField] private float waitingTime;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponentInChildren<Rigidbody2D>();

            _rb.constraints |= RigidbodyConstraints2D.FreezePositionY; // set freeze y
            _rb.constraints |= RigidbodyConstraints2D.FreezeRotation; // set freeze rot
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled)
                return;

            if (!other.transform.TryGetComponent<PlayerTag>(out _))
                return;

            StartCoroutine(WaitAndThrow());
            enabled = false;
        }

        private IEnumerator WaitAndThrow()
        {
            yield return new WaitForSeconds(waitingTime);

            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; // remove freeze y
            _rb.velocity = startVel;
        }
    }
}