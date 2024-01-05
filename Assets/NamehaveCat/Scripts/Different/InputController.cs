namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Direction;
    using UnityEngine;
    using UnityEngine.Events;

    public class InputController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Direction> onMove = new();

        [SerializeField] private KeyCode keyLeft;
        [SerializeField] private KeyCode keyRight;
        [SerializeField] private KeyCode keyUp;

        private Direction _dir;

        private void Start()
        {
            GameManager.Instance.UiManager.BtnLeft.onPressed.AddListener(() => _dir |= Direction.Left);
            GameManager.Instance.UiManager.BtnRight.onPressed.AddListener(() => _dir |= Direction.Right);
            GameManager.Instance.UiManager.BtnUp.onPressed.AddListener(() => _dir |= Direction.Up);
        }

        private void Update()
        {
            if (Input.GetKey(keyLeft)) _dir |= Direction.Left;
            if (Input.GetKey(keyRight)) _dir |= Direction.Right;
            if (Input.GetKey(keyUp)) _dir |= Direction.Up;

            onMove.Invoke(_dir);
            _dir = 0;
        }

        private void OnDisable()
        {
            GameManager.Instance.UiManager.BtnLeft.onPressed.RemoveListener(() => _dir |= Direction.Left);
            GameManager.Instance.UiManager.BtnRight.onPressed.RemoveListener(() => _dir |= Direction.Right);
            GameManager.Instance.UiManager.BtnUp.onPressed.RemoveListener(() => _dir |= Direction.Up);
        }
    }
}