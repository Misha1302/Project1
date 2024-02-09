namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Different.Input;
    using NamehaveCat.Scripts.Entities.Player;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using NamehaveCat.Scripts.UI;
    using UnityEngine;

    public class GameManager : MonoBehSingleton<GameManager>
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private InputController inputController;
        [SerializeField] private Health.Health playerHealth;
        [SerializeField] private PlayerDeath playerDeath;
        [SerializeField] private Pause pause;
        [SerializeField] private MTime time;
        [SerializeField] private CoroutineManager coroutineManager;
        [SerializeField] private CameraScreen cameraScreen;

        public PlayerController PlayerController => playerController;
        public InputController InputController => inputController;
        public Health.Health PlayerHealth => playerHealth;
        public PlayerDeath PlayerDeath => playerDeath;
        public Pause Pause => pause;
        public MTime Time => time;
        public CoroutineManager CoroutineManager => coroutineManager;
        public CameraScreen CameraScreen => cameraScreen;
    }
}