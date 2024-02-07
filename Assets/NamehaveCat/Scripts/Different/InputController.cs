﻿namespace NamehaveCat.Scripts.Different
{
    using System.Collections.Generic;
    using System.Linq;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class InputController : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Direction> onPress = new();

        [SerializeField] private Key[] keysLeft = { Key.A, Key.LeftArrow };
        [SerializeField] private Key[] keysRight = { Key.D, Key.RightArrow };
        [SerializeField] private Key[] keysUp = { Key.Space, Key.UpArrow };

        [SerializeField] private RButton btnLeft;
        [SerializeField] private RButton btnRight;
        [SerializeField] private RButton btnUp;

        private readonly Dictionary<Direction, Axis> _axes = new();
        private Direction _dir;
        public IReadOnlyDictionary<Direction, Axis> Axes => _axes;

        private void Awake()
        {
            InstantiateAxes();

            _axes[Direction.Left].onPressed.AddListener(() => _dir = _dir.Add(Direction.Left));
            _axes[Direction.Right].onPressed.AddListener(() => _dir = _dir.Add(Direction.Right));
            _axes[Direction.Up].onPressed.AddListener(() => _dir = _dir.Add(Direction.Up));
        }

        private void Update()
        {
            onPress.Invoke(_dir);
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