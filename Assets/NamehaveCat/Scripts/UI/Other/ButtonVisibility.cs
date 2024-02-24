namespace NamehaveCat.Scripts.UI.Other
{
    using System.Linq;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    public class ButtonVisibility : MonoBehaviour
    {
        [SerializeField] private Graphic[] colors;

        private float[] _oldVisibility;

        private void Start()
        {
            _oldVisibility = colors.Select(x => x.color.a).ToArray();
            SetVisibility(GameDynamicData.ButtonsAreVisible.get());
            GameDynamicData.ButtonsAreVisible.onSet.AddListener(SetVisibility);
        }

        private void OnValidate()
        {
            if (colors == null || colors.Length == 0)
                colors = GetComponentsInChildren<Graphic>();
        }

        private void SetVisibility(bool visible)
        {
            colors.ForEach((x, i) =>
            {
                if (visible)
                {
                    x.color = x.color.WithA(_oldVisibility[i]);
                }
                else
                {
                    _oldVisibility[i] = x.color.a;
                    x.color = x.color.WithA(0f);
                }
            });
        }
    }
}