namespace NamehaveCat.Scripts.Health
{
    using System;
    using UnityEngine;

    [Serializable]
    public class DamageInfo
    {
        [SerializeField] private float damageSerialized;
        [SerializeField] private string messageSerialized;
        [SerializeField] private DamageType damageTypeSerialized;

        public DamageInfo(float damage, string message, DamageType damageType)
        {
            damageSerialized = damage;
            messageSerialized = message;
            damageTypeSerialized = damageType;
        }

        public float Damage => damageSerialized;
        public string Message => messageSerialized;
        public DamageType DamageType => damageTypeSerialized;
    }
}