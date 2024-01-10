namespace NamehaveCat.Scripts.Lift
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class Lift : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;

        private LiftDirection _direction;
        private bool _havePlayer;

        private Vector3 Destination => _direction == LiftDirection.Top ? top.position : bottom.position;

        private void Update()
        {
            var vec = Destination * (Time.deltaTime * speed);

            transform.Translate(vec);
            if (_havePlayer)
                GameManager.Instance.PlayerController.transform.Translate(vec);

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

        private void OnCollisionEnter2D(Collision2D other) =>
            _havePlayer = _havePlayer || other.transform.TryGetComponent<PlayerTag>(out _);

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<PlayerTag>(out _))
                _havePlayer = false;
        }
    }
}