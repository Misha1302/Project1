namespace NamehaveCat.Scripts.UI.GlobalSettings
{
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Slider))]
    public class MusicSlider : MonoBehaviour
    {
        private void Start()
        {
            var slider = GetComponent<Slider>();
            slider.value = GameDynamicData.MusicValue.get();
            slider.onValueChanged.AddListener(value => GameDynamicData.MusicValue.set(value));
        }
    }
}