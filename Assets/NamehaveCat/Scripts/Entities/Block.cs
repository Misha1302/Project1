namespace NamehaveCat.Scripts.Entities
{
    using System.Collections;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.MImplementations;
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

            GameManager.Instance.CoroutineManager.StartCoroutine(SlowUpdate());
        }

        private IEnumerator SlowUpdate()
        {
            while (true)
            {
                var len = Physics2D.BoxCastNonAlloc(transform.position, scale, 0, Vector2.zero, _results);

                var anyPlayer = false;

                for (var i = 0; i < len; i++)
                {
                    if (!_results[i].transform.TryGetComponent<PlayerTag>(out _))
                        continue;

                    anyPlayer = true;
                    break;
                }

                if (!anyPlayer)
                    _rb.velocity = _rb.velocity.WithX(_rb.velocity.x * slowdownSpeed);

                yield return new MWaitForSeconds(0.1f);
            }

            // ReSharper disable once IteratorNeverReturns
        }
    }
}