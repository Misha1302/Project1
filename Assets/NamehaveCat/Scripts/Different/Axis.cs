namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Velocipedi;
    using UnityEngine;
    using UnityEngine.Events;

    public class Axis : MonoBehaviour
    {
        public readonly UnityEvent onEnd = new();
        public readonly UnityEvent onPressed = new();
        public readonly UnityEvent onStart = new();

        private float _axisStartTime;
        private KeyCode[] _keys;

        private void Update()
        {
            foreach (var key in _keys)
                if (Input.GetKeyDown(key)) OnStart();
                else if (Input.GetKey(key)) OnPressed();
                else if (Input.GetKeyUp(key)) OnEnd();
        }

        public float AsFloat(float duration) => duration - (Time.time - _axisStartTime);

        public static Axis CreateInstance(RButton button, KeyCode[] keys) =>
            new GameObject().AddComponent<Axis>().Init(button, keys);

        public Axis Init(RButton button, KeyCode[] keys)
        {
            _keys = keys;

            button.onStart.AddListener(OnStart);
            button.onPressed.AddListener(OnPressed);
            button.onEnd.AddListener(OnEnd);

            return this;
        }


        private void OnEnd()
        {
            _axisStartTime = 0;
            onEnd.Invoke();
        }

        private void OnPressed()
        {
            onPressed.Invoke();
        }

        private void OnStart()
        {
            _axisStartTime = Time.time;
            onStart.Invoke();
        }

        public void ResetAxis()
        {
            _axisStartTime = Time.time;
        }
    }
}