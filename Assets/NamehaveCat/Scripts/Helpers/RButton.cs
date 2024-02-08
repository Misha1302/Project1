namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;

    public class RButton : Button
    {
        public readonly UnityEvent onEnd = new();
        public readonly UnityEvent onPressed = new();
        public readonly UnityEvent onStart = new();

        private bool _pressed;

        private void Update()
        {
            if (_pressed)
                onPressed.Invoke();
        }

        protected override void OnDisable()
        {
            _pressed = false;
            base.OnDisable();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnPointerUp(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (Touchscreen.current?.touches.Count != 0 || Mouse.current.leftButton.isPressed)
                OnPointerDown(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            base.OnPointerDown(eventData);
            onStart.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;
            base.OnPointerUp(eventData);
            onEnd.Invoke();
        }
    }
}