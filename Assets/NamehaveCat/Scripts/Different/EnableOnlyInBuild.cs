namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public class EnableOnlyInBuild : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(!Application.isEditor);
        }
    }
}