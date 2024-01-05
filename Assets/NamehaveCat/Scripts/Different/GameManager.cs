﻿namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Player;
    using UnityEngine;

    public class GameManager : MonoBehSingleton<GameManager>
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputController inputController;
        [SerializeField] private Health playerHealth;

        public UiManager UiManager => uiManager;
        public PlayerController PlayerController => playerController;
        public InputController InputController => inputController;
        public Health PlayerHealth => playerHealth;
    }
}