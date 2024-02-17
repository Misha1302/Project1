namespace NamehaveCat.Scripts.Health
{
    using System;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.Events;

    public class Health : MonoBehaviour
    {
        [SerializeField] private float startValue = 100;

        public readonly UnityEvent<Health> onDamage = new();

        public DamageInfo DamageInfo { get; private set; }

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

        public void Damage(DamageInfo damageInfo)
        {
            if (!IsValid(damageInfo))
                return;

            DamageInfo = damageInfo;

            Value -= damageInfo.Damage;

            onDamage.Invoke(this);
        }

        private bool IsValid(DamageInfo info)
        {
            if (string.IsNullOrWhiteSpace(info.Message))
            {
                Debug.LogWarning($"Message is {(info.Message == null ? "null" : "whitespace")}");
                return false;
            }

            if (info.Damage <= 0)
            {
                Debug.LogWarning($"Damage was eq {info.Damage}");
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