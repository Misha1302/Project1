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
            // execute in next frame 'cause GameManager initializing in Awake, InputController in Start, PlayerJumper in next frame
            // ExecuteInNextFrame.Instance.Execute(ResetBuffer);
            GameManager.Instance.ExecutorInNextFrame.Execute(() =>
                GameManager.Instance.InputController.Axes[Direction.Up]
                    .onEnd.AddListener(() => _isJumping = false)
            );
        }

        private void ResetBuffer()
        {
            _buffer = StandardBuffer;
        }

        public void TryJump(bool isGrounded)
        {
            if (isGrounded)
            {
                ResetBuffer();
                _isJumping = false;
            }

            if (_isJumping)
                return;

            if (_buffer <= 0)
                return;

            Jump();

            _buffer--;
            _isJumping = true;
        }

        private void Jump()
        {
            GameManager.Instance.ExecutorInNextFrame.Execute(() =>
            {
                var rb = GameManager.Instance.PlayerController.Rb2D;
                rb.velocity = rb.velocity.WithY(speed);
            });
        }
    }
}