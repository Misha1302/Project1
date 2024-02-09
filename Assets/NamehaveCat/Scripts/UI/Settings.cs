namespace NamehaveCat.Scripts.UI
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;
    using UnityEngine.UI;

    public class Settings : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private RawImage settingsFrameImage;

        private void Start()
        {
            settingsBtn.onClick.AddListener(PauseOrRelease);

            restart.onClick.AddListener(MSceneManager.Reload);
        }

        private void PauseOrRelease()
        {
            if (!StateManager.CanPause)
                return;

            if (!settingsPanel.activeSelf)
            {
                settingsFrameImage.texture = GameManager.Instance.CameraScreen.TakeScreen();
                GameManager.Instance.Pause.MPause();
            }
            else
            {
                if (GameManager.Instance.Pause.IsPause)
                    GameManager.Instance.Pause.MRelease();
            }

            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}