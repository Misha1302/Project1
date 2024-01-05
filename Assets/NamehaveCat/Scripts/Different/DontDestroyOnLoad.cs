namespace NamehaveCat.Scripts.Different
{
    using System;
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

            if (transform.parent != null)
                throw new InvalidOperationException();

            _created = true;
            DontDestroyOnLoad(gameObject);
        }
    }
}