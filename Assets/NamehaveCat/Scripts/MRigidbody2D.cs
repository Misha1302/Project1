namespace NamehaveCat.Scripts
{
    using JetBrains.Annotations;
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class MRigidbody2D : MonoBehaviour
    {
        private RigidbodyConstraints2D _constraints;
        private float _mass;
        private bool _needToBeRestored;
        [CanBeNull] private Rigidbody2D _rigidbody2D;
        private Vector2 _vel;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            GameManager.Instance.Pause.onPause.AddListener(OnPauseHandler);
            GameManager.Instance.Pause.onRelease.AddListener(OnReleaseHandler);
        }

        private void OnReleaseHandler()
        {
            if (_rigidbody2D == null || !_needToBeRestored)
                return;

            _rigidbody2D.velocity = _vel;
            _rigidbody2D.constraints = _constraints;
            _rigidbody2D.mass = _mass;
            _needToBeRestored = false;
        }

        private void OnPauseHandler()
        {
            if (_rigidbody2D == null)
                return;

            _vel = _rigidbody2D.velocity;
            _constraints = _rigidbody2D.constraints;
            _mass = _rigidbody2D.mass;
            _needToBeRestored = true;
        }
    }
}