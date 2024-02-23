namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public class SpeedLimiter
    {
        private readonly Rigidbody2D _rb2D;
        private readonly CollisionDetector _collisionDetector;
        private readonly float _maxSpeed;

        public SpeedLimiter(Rigidbody2D rb2D, CollisionDetector collisionDetector, float maxSpeed)
        {
            _rb2D = rb2D;
            _collisionDetector = collisionDetector;
            _maxSpeed = maxSpeed;
        }

        public void LimitHorizontal(Direction _)
        {
            _rb2D.velocity = _rb2D.velocity.WithX(Math.Clamp(_rb2D.velocity.x, -_maxSpeed, _maxSpeed));

            if ((_rb2D.velocity.x < 0 && _collisionDetector.HasObjectOnLeft()) ||
                (_rb2D.velocity.x > 0 && _collisionDetector.HasObjectOnRight()))
                _rb2D.velocity = _rb2D.velocity.WithX(0);
        }
    }
}