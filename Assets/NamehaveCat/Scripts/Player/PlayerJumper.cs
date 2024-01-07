namespace NamehaveCat.Scripts.Player
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using NamehaveCat.Scripts.Enemy;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    // The main feature is to jump with different heights depending on the duration of pressing the button
    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float duration = 1f;
        [SerializeField] private float speed = 9.6f;
        [SerializeField] private float downSpeed = 8f;
        [SerializeField] private int standardBuffer = 1;

        private int _buffer;
        private bool _isJumping;

        private void Start()
        {
            // execute in next frame 'cause GameManager initializing in Awake, InputController in Start, PlayerJumper in next frame
            ExecuteInNextFrame.Instance.Execute(ResetBuffer);
        }

        private void Update()
        {
            if (!_isJumping)
                return;

            var vel = GameManager.Instance.PlayerController.Rb2D.velocity;
            var upAxis = GameManager.Instance.InputController.axes[Direction.Up].AsFloat(duration);

            // if the button is released
            if (upAxis < 0.1f)
            {
                _isJumping = false;
                StartCoroutine(Slowdown());
            }
            else
            {
                // if the speeds are close to each other or if they have just started jumping
                if (vel.y.Eq(upAxis * speed, 0.5f) || upAxis.Eq(duration, 0.2f))
                    vel.y = upAxis * speed;
                else _isJumping = false; // otherwise, we've crashed into something
            }

            GameManager.Instance.PlayerController.Rb2D.velocity = vel;
        }

        private IEnumerator Slowdown()
        {
            Vector2 vel;
            do
            {
                vel = GameManager.Instance.PlayerController.Rb2D.velocity;
                vel.y -= Time.deltaTime * downSpeed;
                GameManager.Instance.PlayerController.Rb2D.velocity = vel;
                
                yield return new WaitForEndOfFrame();
            } while (vel.y >= 0.1f);
        }

        private void ResetBuffer()
        {
            _buffer = standardBuffer;
            GameManager.Instance.InputController.axes[Direction.Up].ResetAxis();
        }

        public void TryJump(bool isGrounded)
        {
            if (_isJumping)
                return;

            if (isGrounded)
                ResetBuffer();

            if (_buffer <= 0)
                return;


            _buffer--;
            _isJumping = true;
        }
    }
}