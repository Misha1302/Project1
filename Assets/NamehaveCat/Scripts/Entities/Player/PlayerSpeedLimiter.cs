namespace NamehaveCat.Scripts.Entities.Player
{
    using System;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    public class PlayerSpeedLimiter : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 4f;

        private void Start()
        {
            GameManager.Instance.InputController.onPress.AddListener(LimitHorizontal);
        }

        private void LimitHorizontal(Direction unused)
        {
            var rb2D = GameManager.Instance.PlayerController.Rb2D;
            rb2D.velocity = rb2D.velocity.WithX(Math.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed));
        }
    }
}