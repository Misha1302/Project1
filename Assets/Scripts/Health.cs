using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Health> onDamage;
    [SerializeField] private float startValue = 100;

    public double Value { get; private set; }
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

    public void Damage(int damage)
    {
        Value -= damage;

        PreviousDamage = damage;
        onDamage.Invoke(this);
    }
}