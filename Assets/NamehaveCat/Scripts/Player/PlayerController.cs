using static NamehaveCat.Scripts.Direction.Direction;

namespace NamehaveCat.Scripts.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(PlayerJumper))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private GroundChecker groundChecker;

        private ObjectFlipper _flipper;
        private PlayerJumper _playerJumper;
        public GroundChecker GroundChecker => groundChecker;

        public Rigidbody2D Rb2D { get; private set; }

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
            var vel = Rb2D.velocity;

            // if no left and right or have left and right
            if ((!dir.Has(Left) && !dir.Has(Right)) || (dir.Has(Left) && dir.Has(Right)))
            {
                vel.x = 0;
            }
            else // if left or right
            {
                if (dir.Has(Left)) // if left
                {
                    vel.x = -speed;
                    _flipper.FlipX = true;
                }
                else if (dir.Has(Right)) // if right
                {
                    vel.x = speed;
                    _flipper.FlipX = false;
                }
            }

            // if up 
            if (dir.Has(Up))
                _playerJumper.TryJump(groundChecker.IsGrounded);

            Rb2D.velocity = vel;
        }
    }
}