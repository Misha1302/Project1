namespace NamehaveCat.Scripts.UI
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    public class Settings : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Pause pause;
        [SerializeField] private CameraScreen cameraScreen;

        private void Start()
        {
            settingsBtn.onClick.AddListener(PauseOrRelease);

            restart.onClick.AddListener(RSceneManager.Reload);
        }

        private void OnValidate()
        {
            if (cameraScreen == null)
                cameraScreen = FindObjectOfType<CameraScreen>();
        }

        private void PauseOrRelease()
        {
            if (!StateManager.CanPause) 
                return;

            if (!settingsPanel.activeSelf)
            {
                GameManager.Instance.UiManager.SettingsFrameImage.texture = cameraScreen.TakeScreen();
                pause.MPause();
            }
            else
            {
                if (pause.IsPause) 
                    pause.MRelease();
            }

            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}