﻿namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    public class SpeedLimiter : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 4f;

        private Rigidbody2D _rb2D;

        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();

            GameManager.Instance.InputController.onPress.AddListener(_ => LimitHorizontal());
        }

        private void LimitHorizontal()
        {
            _rb2D.velocity = _rb2D.velocity.WithX(Math.Clamp(_rb2D.velocity.x, -maxSpeed, maxSpeed));
        }
    }
}