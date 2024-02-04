namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using System;
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Snowball : MonoBehaviour
    {
        private Vector2 _direction;

        private Rigidbody2D _rb2D;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb2D.velocity = _direction;
        }

        public void Set(LayerMask enemy, Vector3 position, Vector3 direction, bool right)
        {
            _rb2D.excludeLayers = enemy;
            _rb2D.position = position;
            _rb2D.velocity = _direction = direction;
            SetRotation(_rb2D.position, _rb2D.velocity, right);
        }

        private void SetRotation(Vector2 pos, Vector2 vel, bool right)
        {
            var point1 = pos;
            var point2 = pos + vel;

            var ba = point2.y - point1.y;
            var bc = Vector2.Distance(point2, point1);

            var angle = MathF.Asin(ba / bc);

            _rb2D.rotation = vel.x < 0 ? -angle : angle;
            _spriteRenderer.flipX = !right;
        }
    }
}