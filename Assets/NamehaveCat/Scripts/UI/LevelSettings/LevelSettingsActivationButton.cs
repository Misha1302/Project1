namespace NamehaveCat.Scripts.UI.LevelSettings
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class LevelSettingsActivationButton : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private RawImage settingsFrameImage;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(PauseOrRelease);
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

                var globalSettings = FindObjectOfType<GlobalSettingsPanelTag>();
                if (globalSettings) globalSettings.gameObject.SetActive(false);
            }

            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}