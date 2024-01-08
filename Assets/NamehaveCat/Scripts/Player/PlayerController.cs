using static NamehaveCat.Scripts.Direction.Direction;

namespace NamehaveCat.Scripts.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(PlayerJumper))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float force = 4_000f;
        [SerializeField] private float forceInFly = 1_000f;
        [SerializeField] private float maxSpeed = 4f;
        [SerializeField] private GroundChecker groundChecker;

        private ObjectFlipper _flipper;
        private PlayerJumper _playerJumper;
        public GroundChecker GroundChecker => groundChecker;

        public Rigidbody2D Rb2D { get; private set; }

        private float Speed => groundChecker.IsGrounded ? force : forceInFly;

        private void Start()
        {
            _playerJumper = GetComponent<PlayerJumper>();
            _flipper = GetComponent<ObjectFlipper>();
            Rb2D = GetComponent<Rigidbody2D>();
            GameManager.Instance.InputController.onMove.AddListener(Move);
        }

        private void OnEnable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.InputController.onMove.AddListener(Move);
            GetComponent<Collider2D>().enabled = true;
            if (Rb2D != null) Rb2D.WakeUp();
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.InputController.onMove.RemoveListener(Move);
            GetComponent<Collider2D>().enabled = false;
            Rb2D.Sleep();
        }

        private void Move(Direction dir)
        {
            // if (no left and right) OR (have left and right) AND (is grounded)
            if (((!dir.Has(Left) && !dir.Has(Right)) || (dir.Has(Left) && dir.Has(Right))) && groundChecker.IsGrounded)
            {
                Rb2D.velocity = Rb2D.velocity.WithX(0);
            }
            else // if left or right
            {
                if (dir.Has(Left)) // if left
                {
                    Rb2D.AddForce(Vector2.left * Speed * Time.deltaTime);
                    _flipper.FlipX = true;
                }
                else if (dir.Has(Right)) // if right
                {
                    Rb2D.AddForce(Vector2.right * Speed * Time.deltaTime);
                    _flipper.FlipX = false;
                }
            }

            LimitSpeed();


            // if up 
            if (dir.Has(Up))
                _playerJumper.TryJump(groundChecker.IsGrounded);
        }

        private void LimitSpeed()
        {
            Rb2D.velocity = Rb2D.velocity.WithX(Math.Clamp(Rb2D.velocity.x, -maxSpeed, maxSpeed));
        }
    }
}