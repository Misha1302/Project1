using static NamehaveCat.Scripts.Direction;

namespace NamehaveCat.Scripts
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ObjectFlipper))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 4;
        [SerializeField] private float jumpForce = 8;
        [SerializeField] private GroundChecker groundChecker;

        private ObjectFlipper _flipper;
        public GroundChecker GroundChecker => groundChecker;

        public Rigidbody2D Rb2D { get; private set; }

        private void Start()
        {
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

            // if up and isGrounded
            if (dir.Has(Up) && groundChecker.IsGrounded)
                vel.y = jumpForce;

            Rb2D.velocity = vel;
        }
    }
}