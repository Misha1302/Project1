namespace NamehaveCat.Scripts.Player
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private float time = 1f;
        [SerializeField] private float height = 9.6f;
        [SerializeField] private float downSpeed = 8f;
        [SerializeField] private int standardBuffer = 1;

        private int _buffer;

        private bool _isJumping;

        private void Start()
        {
            ResetBuffer();
        }

        private void Update()
        {
            if (!_isJumping)
                return;

            var vel = GameManager.Instance.PlayerController.Rb2D.velocity;
            var upAxis = GameManager.Instance.InputController.UpAxis(time);

            if (upAxis < 0.2f)
            {
                _isJumping = false;
                StartCoroutine(SlowJump());
            }
            else
            {
                vel.y = upAxis * (height / time);
            }

            GameManager.Instance.PlayerController.Rb2D.velocity = vel;
        }

        private IEnumerator SlowJump()
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
            GameManager.Instance.InputController.ResetUpAxis();
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