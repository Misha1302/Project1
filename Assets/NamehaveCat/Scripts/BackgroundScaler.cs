using UnityEngine;

namespace NamehaveCat.Scripts
{
    public class BackgroundScaler : MonoBehaviour
    {
        private void Awake()
        {
            // ReSharper disable once ArrangeRedundantParentheses
            transform.localScale *= (Screen.width / (float)Screen.height) / (1920 / 1080f);
        }
    }
}