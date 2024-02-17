namespace NamehaveCat.Scripts.Machinery
{
    using System.Collections;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    [RequireComponent(typeof(MRigidbody2D))]
    public class PistonPushPart : ElectricitySource
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform firstPoint;
        [SerializeField] private Transform secondPoint;

        private MRigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<MRigidbody2D>();
        }

        private void Update()
        {
            // TODO: check for inverted piston (right to left)
            if (!HasElectricity && transform.position.x > secondPoint.position.x)
                GameManager.Instance.CoroutineManager.StartCoroutine(Push());
        }

        private IEnumerator Push()
        {
            var c = _rigidbody2D.mRbConstraints2D;
            var startTime = GameManager.Instance.Time.CurTime;

            HasElectricity = true;
            _rigidbody2D.mRbConstraints2D = MRbConstraints2D.All;

            while (GetT() < 1)
            {
                _rigidbody2D.Teleport(
                    transform.position = Vector3.Lerp(
                        secondPoint.position,
                        firstPoint.position,
                        GetT()
                    )
                );

                yield return null;
            }

            _rigidbody2D.mRbConstraints2D = c;
            HasElectricity = false;

            yield break;


            float GetT() => (GameManager.Instance.Time.CurTime - startTime) * speed;
        }
    }
}