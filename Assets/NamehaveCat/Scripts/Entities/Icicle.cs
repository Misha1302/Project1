namespace NamehaveCat.Scripts.Entities
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.MImplementations;
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

            Freeze();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled)
                return;

            if (!other.transform.TryGetComponent<PlayerTag>(out _))
                return;

            GameManager.Instance.CoroutineManager.StartCoroutine(WaitAndThrow());
            enabled = false;
        }

        private void Freeze()
        {
            var constraints = _rb.constraints;
            constraints |= RigidbodyConstraints2D.FreezePositionY; // set freeze y
            constraints |= RigidbodyConstraints2D.FreezePositionX; // set freeze y
            constraints |= RigidbodyConstraints2D.FreezeRotation; // set freeze rot
            _rb.constraints = constraints;
        }

        private IEnumerator WaitAndThrow()
        {
            yield return new MWaitForSeconds(waitingTime);

            UnfreezePosY();
            _rb.velocity = startVel;
        }

        private void UnfreezePosY()
        {
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY; // remove freeze y
        }
    }
}