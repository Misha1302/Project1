namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public class EnableOnlyInBuild : MonoBehaviour
    {
        [SerializeField] private bool active = true;

        private void Awake()
        {
            if (!active)
                return;

            gameObject.SetActive(!Application.isEditor);
        }
    }
}