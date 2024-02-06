namespace NamehaveCat.Scripts.Machinery
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(IMovable))]
    public class StickyPlatform : MonoBehaviour
    {
        private readonly HashSet<Rigidbody2D> _rbs = new();
        private IMovable _movable;

        private void Start()
        {
            _movable = GetComponent<IMovable>();
            _movable.OnMove.AddListener(OnMove);
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

        private void OnMove(Vector2 vec)
        {
            foreach (var rb in _rbs)
                rb.transform.Translate(vec);
        }
    }
}