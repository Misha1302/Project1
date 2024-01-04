namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class GameManager : MonoBehSingleton<GameManager>
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputController inputController;
        [SerializeField] private Health playerHealth;

        public UiManager UiManager => uiManager;
        public PlayerController PlayerController => playerController.enabled ? playerController : null;
        public InputController InputController => inputController.enabled ? inputController : null;
        public Health PlayerHealth => playerHealth.enabled ? playerHealth : null;
    }
}