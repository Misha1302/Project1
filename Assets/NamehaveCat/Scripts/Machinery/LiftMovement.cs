namespace NamehaveCat.Scripts.Machinery
{
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(StickyPlatform))]
    public class LiftMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;

        private LiftDirection _direction;

        private Vector2 Direction => _direction == LiftDirection.Top ? Vector2.up : Vector2.down;

        private void Update()
        {
            var vec = Direction * (Time.deltaTime * speed);

            transform.Translate(vec);
            OnMove.Invoke(vec);

            ChangeDirectionIfNeed();
        }

        private void ChangeDirectionIfNeed()
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

        public UnityEvent<Vector2> OnMove { get; } = new();
    }
}