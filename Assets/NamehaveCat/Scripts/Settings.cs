namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Velocipedi;
    using UnityEngine;
    using UnityEngine.UI;

    public class Settings : MonoBehaviour
    {
        [SerializeField] private Button restart;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Pause pause;

        private void Start()
        {
            settingsBtn.onClick.AddListener(() =>
            {
                if (!settingsPanel.activeSelf)
                    pause.MPause();
                else pause.MRelease();
                
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            });

            restart.onClick.AddListener(RSceneManager.Reload);
        }
    }
}