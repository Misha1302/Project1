namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    // The main feature is to jump with different heights depending on the duration of pressing the button
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float speed = 9f;
        [SerializeField] private int standardBuffer = 1;

        private int _buffer;
        private bool _isJumping;

        public int StandardBuffer
        {
            get => standardBuffer;
            // ReSharper disable once UnusedMember.Global
            set
            {
                if (value < 0)
                    Thrower.Throw(
                        new ArgumentOutOfRangeException(nameof(value), "Value must be greater than or equal to zero")
                    );

                standardBuffer = value;
            }
        }

        private void Start()
        {
            GameManager.Instance.InputController.Axes[Direction.Up]
                .onEnd.AddListener(() => _isJumping = false);
        }

        private void ResetBuffer()
        {
            _buffer = StandardBuffer;
        }

        public bool TryJump()
        {
            if (GameManager.Instance.PlayerController.GroundChecker.CanJump)
            {
                ResetBuffer();
                _isJumping = false;
            }

            if (!CanJump())
                return false;

            Jump();

            _buffer--;
            _isJumping = true;

            return true;
        }

        private bool CanJump() => !_isJumping && _buffer > 0;

        private void Jump()
        {
            var rb = GameManager.Instance.PlayerController.Rb2D;
            rb.velocity = rb.velocity.WithY(speed);
        }
    }
}