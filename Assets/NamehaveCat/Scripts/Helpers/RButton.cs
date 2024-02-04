namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class RButton : Button
    {
        [HideInInspector] public UnityEvent onPressed = new();
        [HideInInspector] public UnityEvent onStart = new();
        [HideInInspector] public UnityEvent onEnd = new();

        private bool _pressed;

        private void Update()
        {
            if (_pressed)
                onPressed.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            base.OnPointerDown(eventData);
            onStart.Invoke();
        }

        protected override void OnDisable()
        {
            _pressed = false;
            base.OnDisable();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            base.OnPointerUp(eventData);
            onEnd.Invoke();
        }
    }
}