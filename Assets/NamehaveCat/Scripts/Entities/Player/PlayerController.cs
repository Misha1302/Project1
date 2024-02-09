using static NamehaveCat.Scripts.Different.Direction;

namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(PlayerJumper))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float force = 8000f / 3f; // 2666.6667
        [SerializeField] private float forceInFly = 1_000f;
        [SerializeField] private float maxSpeed = 4f;
        [SerializeField] private GroundChecker groundChecker;

        private Collider2D _collider;
        private ObjectFlipper _flipper;
        private PlayerJumper _playerJumper;
        public GroundChecker GroundChecker => groundChecker;

        public Rigidbody2D Rb2D { get; private set; }

        private float Speed => groundChecker.IsGrounded ? force : forceInFly;

        private void Start()
        {
            GameManager.Instance.InputController.onPress.AddListener(Move);
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.InputController.onPress.AddListener(Move);

            Init();

            _collider.enabled = true;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.InputController.onPress.RemoveListener(Move);

            _collider.enabled = false;
        }

        private void Init()
        {
            if (_collider == null) _collider = GetComponent<Collider2D>();
            if (Rb2D == null) Rb2D = GetComponent<Rigidbody2D>();
            if (_playerJumper == null) _playerJumper = GetComponent<PlayerJumper>();
            if (_flipper == null) _flipper = GetComponent<ObjectFlipper>();
        }

        private void Move(Direction dir)
        {
            Horizontal(dir);
            Vertical(dir);
            LimitHorizontal();
        }

        private void Vertical(Direction dir)
        {
            if (dir.Has(Up))
                _playerJumper.TryJump(groundChecker.IsGrounded);
        }

        private void Horizontal(Direction dir)
        {
            // if ((no left and right) or (have left and right)) and (is grounded)
            if (((!dir.Has(Left) && !dir.Has(Right)) || (dir.Has(Left) && dir.Has(Right))) && groundChecker.IsGrounded)
            {
                Rb2D.velocity = Rb2D.velocity.WithX(0);
            }
            else // if left or right
            {
                if (dir.Has(Left)) // if left
                {
                    Rb2D.AddForce(Vector2.left * (Speed * Time.deltaTime));
                    _flipper.FlipX = true;
                }
                else if (dir.Has(Right)) // if right
                {
                    Rb2D.AddForce(Vector2.right * (Speed * Time.deltaTime));
                    _flipper.FlipX = false;
                }
            }
        }

        private void LimitHorizontal()
        {
            Rb2D.velocity = Rb2D.velocity.WithX(Math.Clamp(Rb2D.velocity.x, -maxSpeed, maxSpeed));
        }
    }
}