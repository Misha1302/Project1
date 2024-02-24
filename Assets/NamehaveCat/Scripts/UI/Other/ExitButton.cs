namespace NamehaveCat.Scripts.UI.Other
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => panel.SetActive(false));
        }
    }
}