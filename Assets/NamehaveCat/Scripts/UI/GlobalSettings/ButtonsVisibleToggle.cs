namespace NamehaveCat.Scripts.UI.GlobalSettings
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Toggle))]
    public class ButtonsVisibleToggle : MonoBehaviour
    {
        private void Start()
        {
            var toggle = GetComponent<Toggle>();

            toggle.isOn = GameDynamicData.ButtonsAreVisible.get();
            toggle.onValueChanged.AddListener(value => GameDynamicData.ButtonsAreVisible.set(value));
        }
    }
}