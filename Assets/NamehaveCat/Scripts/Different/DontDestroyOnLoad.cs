namespace NamehaveCat.Scripts.Different
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class DontDestroyOnLoad : MonoBehaviour
    {
        private static readonly List<GameObject> _createdObjects = new();

        private void OnEnable()
        {
            if (_createdObjects.Any(x => x.name == name))
            {
                Destroy(gameObject);
                return;
            }

            transform.SetParent(null);

            _createdObjects.Add(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}