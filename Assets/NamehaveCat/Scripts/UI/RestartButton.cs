namespace NamehaveCat.Scripts.UI
{
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameData.spawnPosition = VectorsExtensions.NaN3;
                MSceneManager.Reload();
            });
        }
    }
}