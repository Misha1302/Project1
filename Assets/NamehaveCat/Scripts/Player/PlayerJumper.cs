namespace NamehaveCat.Scripts.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using NamehaveCat.Scripts.Velocipedi;
    using UnityEngine;

    // The main feature is to jump with different heights depending on the duration of pressing the button
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float speed = 9.6f;
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
            ExecuteInNextFrame.Instance.Execute(ResetBuffer);
            ExecuteInNextFrame.Instance.Execute(() =>
                GameManager.Instance.InputController.axes[Direction.Up].onEnd.AddListener(() => _isJumping = false));
        }

        private void ResetBuffer()
        {
            _buffer = StandardBuffer;
            GameManager.Instance.InputController.axes[Direction.Up].ResetAxis();
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
            var vel = GameManager.Instance.PlayerController.Rb2D.velocity;
            vel.y = speed;
            GameManager.Instance.PlayerController.Rb2D.velocity = vel;
        }
    }
}