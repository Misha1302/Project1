namespace NamehaveCat.Scripts.UI
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

        private (GameObject obj, bool activeSelf)[] _objects;
        public bool IsPause { get; private set; }

        public void MPause()
        {
            IsPause = true;

            onPause.Invoke();

            _objects = FindObjectsOfType<GameObject>()
                .Where(x => !objectsToActive.Contains(x))
                .Where(x => !x.TryGetComponent<DontPauseTag>(out _))
                .Select(x => (x, x.activeSelf))
                .ToArray();

            foreach (var pair in _objects)
                pair.obj.SetActive(false);
        }

        public void MRelease()
        {
            IsPause = false;

            foreach (var pair in _objects)
                if (pair.obj != null)
                    pair.obj.SetActive(pair.activeSelf);

            onRelease.Invoke();
        }
    }
}