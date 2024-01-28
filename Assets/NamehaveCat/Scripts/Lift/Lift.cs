namespace NamehaveCat.Scripts.Lift
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Lift : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;
        private readonly HashSet<Rigidbody2D> _rbs = new();

        private LiftDirection _direction;

        private Vector3 Destination => _direction == LiftDirection.Top ? top.position : bottom.position;

        private void Update()
        {
            var vec = Destination * (Time.deltaTime * speed);

            transform.Translate(vec);

            foreach (var rb in _rbs)
                rb.transform.Translate(vec);

            TryChangeDirection();
        }

        private void TryChangeDirection()
        {
            // ReSharper disable Unity.InefficientPropertyAccess
            _direction = _direction switch
            {
                LiftDirection.Top when top.position.y < transform.position.y => LiftDirection.Bottom,
                LiftDirection.Bottom when bottom.position.y > transform.position.y => LiftDirection.Top,
                _ => _direction
            };
        }

        private enum LiftDirection
        {
            Top,
            Bottom
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Rigidbody2D>(out var rb))
                _rbs.Add(rb);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<Rigidbody2D>(out var rb))
                _rbs.Remove(rb);
        }
    }
}