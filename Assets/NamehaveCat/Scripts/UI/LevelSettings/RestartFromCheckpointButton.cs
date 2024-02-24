namespace NamehaveCat.Scripts.UI.LevelSettings
{
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class RestartFromCheckpointButton : MonoBehaviour
    {
        private void Start()
        {
            // save start pos
            GetComponent<Button>().onClick.AddListener(MSceneManager.Reload);
        }
    }
}