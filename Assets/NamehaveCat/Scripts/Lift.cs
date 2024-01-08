namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class Lift : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;

        private LiftDirection _direction;

        private Vector3 Destination => _direction == LiftDirection.Top ? top.position : bottom.position;

        private void Update()
        {
            transform.Translate(Destination * (Time.deltaTime * speed));
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
    }
}