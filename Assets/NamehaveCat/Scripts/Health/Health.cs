namespace NamehaveCat.Scripts.Health
{
    using System;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour
    {
        [SerializeField] private float startValue = 100;

        [HideInInspector] public UnityEvent<Health> onDamage;

        public string Message { get; private set; }

        public float Value { get; private set; }

        private void Start()
        {
            if (Value == 0)
                InitHealth(startValue);
        }

        private void InitHealth(float value)
        {
            if (value <= 0)
                Thrower.Throw(new ArgumentOutOfRangeException(nameof(value)));

            Value = value;
        }

        public void Damage(float damage, string message)
        {
            if (!Validate(message))
                return;

            Message = message;
            Value -= damage;

            onDamage.Invoke(this);
        }

        private bool Validate(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Debug.LogWarning($"Message is {(message == null ? "null" : "whitespace")}");
                return false;
            }

            if (!enabled)
            {
                Debug.LogWarning("Health disabled!");
                return false;
            }

            return true;
        }
    }
}