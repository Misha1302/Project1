namespace NamehaveCat.Scripts.Different.Input
{
    using System.Collections.Generic;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class Axis : MonoBehaviour
    {
        public readonly UnityEvent onEnd = new();
        public readonly UnityEvent onPressed = new();
        public readonly UnityEvent onStart = new();

        private Key[] _keys;

        private void Update()
        {
            foreach (var key in _keys)
                if (Keyboard.current[key].wasPressedThisFrame) onStart.Invoke();
                else if (Keyboard.current[key].isPressed) onPressed.Invoke();
                else if (Keyboard.current[key].wasReleasedThisFrame) onEnd.Invoke();
        }

        public static Axis CreateInstance(MButton button, Key[] keys) =>
            GameObjectsCreator.New<Axis>(MakeName(button, keys)).Init(button, keys);

        private static string MakeName(Object button, IEnumerable<Key> keys) =>
            $"Axis; Btn: {button.name}; Keys: {string.Join(",", keys)}";

        private Axis Init(MButton button, Key[] keys)
        {
            _keys = keys;

            button.onStart.AddListener(onStart.Invoke);
            button.onPressed.AddListener(onPressed.Invoke);
            button.onEnd.AddListener(onEnd.Invoke);

            return this;
        }
    }
}