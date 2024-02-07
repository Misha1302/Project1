namespace NamehaveCat.Scripts.Different
{
    using System.Collections.Generic;
    using System.Linq;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;

    public class InputController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Direction> onMove = new();

        [SerializeField] private KeyCode[] keysLeft = { KeyCode.A, KeyCode.LeftArrow };
        [SerializeField] private KeyCode[] keysRight = { KeyCode.D, KeyCode.RightArrow };
        [SerializeField] private KeyCode[] keysUp = { KeyCode.Space, KeyCode.UpArrow };

        [SerializeField] private RButton btnLeft;
        [SerializeField] private RButton btnRight;
        [SerializeField] private RButton btnUp;

        private readonly Dictionary<Direction, Axis> _axes = new();
        private Direction _dir;
        public IReadOnlyDictionary<Direction, Axis> Axes => _axes;

        private void Awake()
        {
            InstantiateAxes();

            _axes[Direction.Left].onPressed.AddListener(() => _dir |= Direction.Left);
            _axes[Direction.Right].onPressed.AddListener(() => _dir |= Direction.Right);
            _axes[Direction.Up].onPressed.AddListener(() => _dir |= Direction.Up);
        }

        private void Update()
        {
            onMove.Invoke(_dir);
            _dir = Direction.None;
        }

        private void OnEnable()
        {
            foreach (var (_, axis) in _axes.Where(axis => axis.Value != null))
                axis.enabled = true;
        }

        private void OnDisable()
        {
            // axes may be destroyed while scene reloading
            foreach (var (_, axis) in _axes.Where(axis => axis.Value != null))
                axis.enabled = false;
        }

        private void InstantiateAxes()
        {
            _axes.Add(Direction.Left, Axis.CreateInstance(btnLeft, keysLeft));
            _axes.Add(Direction.Right, Axis.CreateInstance(btnRight, keysRight));
            _axes.Add(Direction.Up, Axis.CreateInstance(btnUp, keysUp));
        }
    }
}