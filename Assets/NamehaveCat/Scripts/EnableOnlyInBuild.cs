using UnityEngine;

namespace NamehaveCat.Scripts
{
    public class EnableOnlyInBuild : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(!Application.isEditor);
        }
    }
}