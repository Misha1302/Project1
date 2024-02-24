namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class SettingsButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                FindObjectOfType<GlobalSettingsPanelTag>(true).gameObject.SetActive(true)
            );
        }
    }
}