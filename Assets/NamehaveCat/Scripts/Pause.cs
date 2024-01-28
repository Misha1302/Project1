namespace NamehaveCat.Scripts
{
    using System.Linq;
    using UnityEngine;

    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToActive;

        private (GameObject obj, bool activeSelf)[] _objects;

        public void MPause()
        {
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
            foreach (var pair in _objects)
                pair.obj.SetActive(pair.activeSelf);
        }
    }
}