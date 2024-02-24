namespace NamehaveCat.Scripts.UI.GlobalSettings
{
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class GlobalSettingsActivationButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                FindObjectOfType<GlobalSettingsPanelTag>(true).gameObject.SetActive(true)
            );
        }
    }
}