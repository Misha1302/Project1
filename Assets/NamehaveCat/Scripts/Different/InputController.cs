namespace NamehaveCat.Scripts.Different
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    public class InputController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Direction> onMove = new();

        [SerializeField] private KeyCode[] keysLeft = { KeyCode.A, KeyCode.LeftArrow };
        [SerializeField] private KeyCode[] keysRight = { KeyCode.D, KeyCode.RightArrow };
        [SerializeField] private KeyCode[] keysUp = { KeyCode.Space, KeyCode.UpArrow };

        public readonly Dictionary<Direction, Axis> axes = new();

        private Direction _dir;

        private void Start()
        {
            InstantiateAxes();

            axes[Direction.Left].onPressed.AddListener(() => _dir |= Direction.Left);
            axes[Direction.Right].onPressed.AddListener(() => _dir |= Direction.Right);
            axes[Direction.Up].onPressed.AddListener(() => _dir |= Direction.Up);
        }

        private void Update()
        {
            onMove.Invoke(_dir);
            _dir = Direction.None;
        }

        private void OnDisable()
        {
            foreach (var (_, axis) in axes.Where(axis => axis.Value != null)) 
                axis.enabled = false;
        }

        private void InstantiateAxes()
        {
            axes.Add(Direction.Left, Axis.CreateInstance(GameManager.Instance.UiManager.BtnLeft, keysLeft));
            axes.Add(Direction.Right, Axis.CreateInstance(GameManager.Instance.UiManager.BtnRight, keysRight));
            axes.Add(Direction.Up, Axis.CreateInstance(GameManager.Instance.UiManager.BtnUp, keysUp));
        }
    }
}