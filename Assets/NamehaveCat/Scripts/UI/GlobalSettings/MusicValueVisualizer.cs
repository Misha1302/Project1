namespace NamehaveCat.Scripts.UI.GlobalSettings
{
    using NamehaveCat.Scripts.Helpers;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class MusicValueVisualizer : MonoBehaviour
    {
        [SerializeField] private string format = "Громкость музыки ({0:0}%)";

        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();

            SetText(GameDynamicData.MusicValue.get());
            GameDynamicData.MusicValue.onSet.AddListener(SetText);
        }

        private void SetText(float value)
        {
            _text.text = string.Format(format, value * 100);
        }
    }
}