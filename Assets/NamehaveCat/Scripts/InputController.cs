namespace NamehaveCat.Scripts
{
    using UnityEngine;
    using UnityEngine.Events;

    public class InputController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Direction> onMove = new();

        [SerializeField] private KeyCode keyLeft;
        [SerializeField] private KeyCode keyRight;
        [SerializeField] private KeyCode keyUp;

        [SerializeField] private RButton btnLeft;
        [SerializeField] private RButton btnRight;
        [SerializeField] private RButton btnUp;

        private Direction _dir;

        private void Update()
        {
            if (Input.GetKey(keyLeft)) _dir |= Direction.Left;
            if (Input.GetKey(keyRight)) _dir |= Direction.Right;
            if (Input.GetKey(keyUp)) _dir |= Direction.Up;

            onMove.Invoke(_dir);
            _dir = 0;
        }

        private void OnEnable()
        {
            btnLeft.onPressed.AddListener(() => _dir |= Direction.Left);
            btnRight.onPressed.AddListener(() => _dir |= Direction.Right);
            btnUp.onPressed.AddListener(() => _dir |= Direction.Up);
        }

        private void OnDisable()
        {
            btnLeft.onPressed.RemoveListener(() => _dir |= Direction.Left);
            btnRight.onPressed.RemoveListener(() => _dir |= Direction.Right);
            btnUp.onPressed.RemoveListener(() => _dir |= Direction.Up);
        }
    }
}