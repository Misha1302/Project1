namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static bool _created;

        private void Start()
        {
            if (_created)
            {
                Destroy(gameObject);
                return;
            }

            transform.parent = null;

            _created = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}