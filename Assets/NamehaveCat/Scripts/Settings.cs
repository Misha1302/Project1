namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Velocipedi;
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
            settingsBtn.onClick.AddListener(() =>
            {
                if (!settingsPanel.activeSelf)
                {
                    GameManager.Instance.UiManager.SettingsFrameImage.texture = cameraScreen.TakeScreen();
                    pause.MPause();
                }
                else
                {
                    pause.MRelease();
                }

                settingsPanel.SetActive(!settingsPanel.activeSelf);
            });

            restart.onClick.AddListener(RSceneManager.Reload);
        }

        private void OnValidate()
        {
            if (cameraScreen == null)
                cameraScreen = FindObjectOfType<CameraScreen>();
        }
    }
}