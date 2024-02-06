namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;

    public class Axis : MonoBehaviour
    {
        public readonly UnityEvent onEnd = new();
        public readonly UnityEvent onPressed = new();
        public readonly UnityEvent onStart = new();

        private KeyCode[] _keys;

        private void Update()
        {
            foreach (var key in _keys)
                if (Input.GetKeyDown(key)) OnStart();
                else if (Input.GetKey(key)) OnPressed();
                else if (Input.GetKeyUp(key)) OnEnd();
        }

        public static Axis CreateInstance(RButton button, KeyCode[] keys) =>
            new GameObject().AddComponent<Axis>().Init(button, keys);

        private Axis Init(RButton button, KeyCode[] keys)
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