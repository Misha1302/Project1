namespace NamehaveCat.Scripts.UI
{
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class SettingsPanelButton : MonoBehaviour
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
            }

            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}