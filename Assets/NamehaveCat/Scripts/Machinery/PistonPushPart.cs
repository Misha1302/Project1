namespace NamehaveCat.Scripts.Machinery
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.MImplementations;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    [RequireComponent(typeof(MRigidbody2D))]
    public class PistonPushPart : ElectricitySource
    {
        [SerializeField] private float duration;
        [SerializeField] private Transform destinationPoint;

        private MRigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<MRigidbody2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!HasElectricity && other.TryGetComponent<PistonBlockPartTag>(out _))
                GameManager.Instance.CoroutineManager.StartCoroutine(Push());
        }

        private IEnumerator Push()
        {
            HasElectricity = true;

            var step = transform.Distance(destinationPoint) / duration / (1 / Time.fixedDeltaTime);

            var waitForFixedUpdate = new WaitForFixedUpdate();

            while (transform.Distance(destinationPoint) >= 0.01f)
            {
                _rigidbody2D.Teleport(transform.MoveTo(destinationPoint, step));

                yield return waitForFixedUpdate;
            }

            HasElectricity = false;
        }
    }
}