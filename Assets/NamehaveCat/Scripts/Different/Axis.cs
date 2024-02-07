namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Helpers;
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
                if (Keyboard.current[key].wasPressedThisFrame) OnStart();
                else if (Keyboard.current[key].isPressed) OnPressed();
                else if (Keyboard.current[key].wasReleasedThisFrame) OnEnd();
        }


        public static Axis CreateInstance(RButton button, Key[] keys) =>
            new GameObject().AddComponent<Axis>().Init(button, keys);

        private Axis Init(RButton button, Key[] keys)
        {
            _keys = keys;

            button.onStart.AddListener(OnStart);
            button.onPressed.AddListener(OnPressed);
            button.onEnd.AddListener(OnEnd);

            return this;
        }

        private void OnEnd() => onEnd.Invoke();
        private void OnPressed() => onPressed.Invoke();
        private void OnStart() => onStart.Invoke();
    }
}