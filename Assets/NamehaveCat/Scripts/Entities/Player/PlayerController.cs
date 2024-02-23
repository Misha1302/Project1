using static NamehaveCat.Scripts.Different.Direction;

namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(ObjectFlipper))]
    [RequireComponent(typeof(PlayerJumper))]
    [RequireComponent(typeof(CollisionDetector))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float force = 8000f / 3f; // 2666.6667
        [SerializeField] private float forceInFly = 1_000f;
        [SerializeField] private GroundChecker groundChecker;

        private Collider2D _collider;
        private ObjectFlipper _flipper;
        private PlayerJumper _playerJumper;
        public GroundChecker GroundChecker => groundChecker;

        public Rigidbody2D Rb2D { get; private set; }
        public CollisionDetector CollisionDetector { get; private set; }

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
            if (CollisionDetector == null) CollisionDetector = GetComponent<CollisionDetector>();
        }

        private void Move(Direction dir)
        {
            Horizontal(dir);
            Vertical(dir);
        }

        private void Vertical(Direction dir)
        {
            if (dir.Has(Up))
                _playerJumper.TryJump();
        }

        private void Horizontal(Direction dir)
        {
            // if is grounded and ((left and right) or (not left and not right))
            if (groundChecker.IsGrounded && dir.Has(Left) == dir.Has(Right))
            {
                Rb2D.velocity = Rb2D.velocity.WithX(0);
            }
            else
            {
                if (dir.Has(Left) && !CollisionDetector.HasObjectOnLeft()) // if left
                {
                    Rb2D.AddForce(Vector2.left * (Speed * GameManager.Instance.Time.DeltaTime));
                    _flipper.FlipX = true;
                }

                if (dir.Has(Right) && !CollisionDetector.HasObjectOnRight()) // if right
                {
                    Rb2D.AddForce(Vector2.right * (Speed * GameManager.Instance.Time.DeltaTime));
                    _flipper.FlipX = false;
                }
            }
        }
    }
}