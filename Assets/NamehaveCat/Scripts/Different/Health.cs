namespace NamehaveCat.Scripts.Different
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Health> onDamage;
        [SerializeField] private float startValue = 100;
        public string Message { get; private set; }

        public float Value { get; private set; }
        public double PreviousDamage { get; private set; }

        private void Start()
        {
            if (Value == 0)
                InitHealth(startValue);
        }

        public void InitHealth(float value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public void Damage(float damage, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Debug.LogWarning($"Message is {(message == null ? "null" : "whitespace")}");
                return;
            }

            if (!enabled)
            {
                Debug.LogWarning("Health disabled!");
                return;
            }

            Message = message;

            Value -= damage;

            PreviousDamage = damage;
            onDamage.Invoke(this);
        }
    }
}