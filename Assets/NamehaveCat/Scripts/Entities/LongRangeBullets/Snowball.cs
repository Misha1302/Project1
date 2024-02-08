namespace NamehaveCat.Scripts.Entities.LongRangeBullets
{
    using System;
    using NamehaveCat.Scripts.Extensions;
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

        public void Set(LayerMask enemy, Vector3 position, Vector3 direction)
        {
            _rb2D.excludeLayers = enemy;
            _rb2D.position = position;
            _rb2D.velocity = _direction = direction;
            SetRotation(_rb2D.position, _rb2D.velocity);
        }

        private void SetRotation(Vector2 pos, Vector2 vel)
        {
            var angle = pos.Degrees(pos + vel);

            // if (x, y < 0) or (x, y > 0)
            var z = MathF.Sign(vel.x) == MathF.Sign(vel.y) ? angle : -angle;
            _rb2D.transform.rotation = Quaternion.Euler(0, 0, z);
            _spriteRenderer.flipX = vel.x > 0;
        }
    }
}