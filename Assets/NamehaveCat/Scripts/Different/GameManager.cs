namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Entities.Player;
    using UnityEngine;

    public class GameManager : MonoBehSingleton<GameManager>
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputController inputController;
        [SerializeField] private Health playerHealth;
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private Pause pause;
        [SerializeField] private MTime time;

        public UiManager UiManager => uiManager;
        public PlayerController PlayerController => playerController;
        public InputController InputController => inputController;
        public Health PlayerHealth => playerHealth;
        public PlayerDeath PlayerDeath => playerDeath;
        public Pause Pause => pause;
        public MTime Time => time;
    }
}