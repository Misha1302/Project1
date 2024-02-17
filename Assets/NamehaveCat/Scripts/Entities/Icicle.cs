namespace NamehaveCat.Scripts.Entities
{
    using NamehaveCat.Scripts.Extensions;
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
            if (!CanThrowIcicle(other))
                return;

            GameManager.Instance.CoroutineManager.InvokeAfter(ThrowIcicle, waitingTime);

            enabled = false;
        }

        private bool CanThrowIcicle(Component other) =>
            other.transform.TryGetComponent<PlayerTag>(out _) && enabled;

        private void ThrowIcicle()
        {
            UnfreezePosY();
            _rb.velocity = startVel;
        }

        private void Freeze()
        {
            _rb.constraints = _rb.constraints
                .Add(RigidbodyConstraints2D.FreezePositionY)
                .Add(RigidbodyConstraints2D.FreezePositionX)
                .Add(RigidbodyConstraints2D.FreezeRotation);
        }

        private void UnfreezePosY()
        {
            _rb.constraints = _rb.constraints.Remove(RigidbodyConstraints2D.FreezePositionY);
        }
    }
}