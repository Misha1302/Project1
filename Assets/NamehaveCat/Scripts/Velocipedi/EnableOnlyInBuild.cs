namespace NamehaveCat.Scripts.Velocipedi
{
    using UnityEngine;

    public class EnableOnlyInBuild : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(!Application.isEditor);
        }
    }
}