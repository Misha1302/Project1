namespace NamehaveCat.Scripts.MImplementations
{
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class MRigidbody2D : MonoBehaviour
    {
        [SerializeField] public MRbConstraints2D mRbConstraints2D;

        private readonly MRbState _state = new();
        private Vector3 _prevPos;


        private void Start()
        {
            _state.rigidbody2D = GetComponent<Rigidbody2D>();
            _prevPos = transform.position;

            GameManager.Instance.Pause.onPause.AddListener(OnPauseHandler);
            GameManager.Instance.Pause.onRelease.AddListener(OnReleaseHandler);
        }

        private void LateUpdate()
        {
            LimitRigidbody();
        }

        private void LimitRigidbody()
        {
            // ReSharper disable Unity.InefficientPropertyAccess
            if (mRbConstraints2D.Has(MRbConstraints2D.LeftX))
                if (transform.position.x < _prevPos.x)
                    transform.position = transform.position.WithX(_prevPos.x);

            if (mRbConstraints2D.Has(MRbConstraints2D.RightX))
                if (transform.position.x > _prevPos.x)
                    transform.position = transform.position.WithX(_prevPos.x);

            if (mRbConstraints2D.Has(MRbConstraints2D.UpY))
                if (transform.position.y > _prevPos.y)
                    transform.position = transform.position.WithY(_prevPos.y);

            if (mRbConstraints2D.Has(MRbConstraints2D.DownY))
                if (transform.position.y < _prevPos.y)
                    transform.position = transform.position.WithY(_prevPos.y);

            _prevPos = transform.position;
        }

        public void Teleport(Vector3 pos)
        {
            _prevPos = pos;
        }

        private void OnReleaseHandler()
        {
            if (_state.rigidbody2D == null || !_state.needToBeRestored)
                return;

            _state.rigidbody2D.velocity = _state.vel;
            _state.rigidbody2D.constraints = _state.constraints;
            _state.rigidbody2D.mass = _state.mass;
            _state.needToBeRestored = false;
        }

        private void OnPauseHandler()
        {
            if (_state.rigidbody2D == null)
                return;

            _state.vel = _state.rigidbody2D.velocity;
            _state.constraints = _state.rigidbody2D.constraints;
            _state.mass = _state.rigidbody2D.mass;
            _state.needToBeRestored = true;
        }
    }
}