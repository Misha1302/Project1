namespace NamehaveCat.Scripts.Different
{
    using System.Linq;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;
    using UnityEngine.Events;

    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToActive;
        [HideInInspector] public UnityEvent onPause = new();
        [HideInInspector] public UnityEvent onRelease = new();

        private GameObject[] _objects;
        public bool IsPause { get; private set; }

        public void MPause()
        {
            IsPause = true;

            onPause.Invoke();

            _objects = FindObjectsOfType<GameObject>()
                .Except(objectsToActive)
                .Where(x => !x.TryGetComponent<DontPauseTag>(out _))
                .ToArray();

            foreach (var pair in _objects)
                pair.SetActive(false);
        }

        public void MRelease()
        {
            IsPause = false;

            foreach (var pair in _objects)
                if (pair != null)
                    pair.SetActive(true);

            onRelease.Invoke();
        }
    }
}