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

        public float upAxisStartTime;

        private void Start()
        {
            GameManager.Instance.UiManager.BtnLeft.onPressed.AddListener(() => _dir |= Direction.Left);
            GameManager.Instance.UiManager.BtnRight.onPressed.AddListener(() => _dir |= Direction.Right);
            GameManager.Instance.UiManager.BtnUp.onPressed.AddListener(() => _dir |= Direction.Up);

            GameManager.Instance.UiManager.BtnUp.onStart.AddListener(() => upAxisStartTime = Time.time);
            GameManager.Instance.UiManager.BtnUp.onEnd.AddListener(() => upAxisStartTime = 0);
        }

        private void Update()
        {
            if (Input.GetKey(keyLeft)) _dir |= Direction.Left;
            if (Input.GetKey(keyRight)) _dir |= Direction.Right;
            if (Input.GetKey(keyUp)) _dir |= Direction.Up;

            if (Input.GetKeyDown(keyUp)) upAxisStartTime = Time.time;
            if (Input.GetKeyUp(keyUp)) upAxisStartTime = 0;

            onMove.Invoke(_dir);
            _dir = 0;
        }

        private void OnDisable()
        {
            GameManager.Instance.UiManager.BtnLeft.onPressed.RemoveAllListeners();
            GameManager.Instance.UiManager.BtnRight.onPressed.RemoveAllListeners();
            GameManager.Instance.UiManager.BtnUp.onPressed.RemoveAllListeners();
        }

        public float UpAxis(float scale) => upAxisStartTime != 0 ? scale - (Time.time - upAxisStartTime) : 0;
    }
}