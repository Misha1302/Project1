namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class Block : MonoBehaviour
    {
        [SerializeField] private Vector2 scale;
        [SerializeField] private float slowdownSpeed;

        private readonly RaycastHit2D[] _results = new RaycastHit2D[128];

        private float _playerCollisions;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var size = Physics2D.BoxCastNonAlloc(transform.position, scale, 0, Vector2.zero, _results);

            var anyPlayer = false;

            // print(string.Join(", ", _results[..size].Select(x => x.transform.name)));
            for (var i = 0; i < size; i++)
            {
                if (!_results[i].transform.TryGetComponent<PlayerTag>(out _))
                    continue;

                anyPlayer = true;
                break;
            }

            if (!anyPlayer)
                _rb.velocity = _rb.velocity.WithX(_rb.velocity.x * slowdownSpeed);
        }
    }
}