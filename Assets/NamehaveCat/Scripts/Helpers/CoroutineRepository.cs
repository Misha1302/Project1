namespace NamehaveCat.Scripts.Helpers
{
    using System.Collections;
    using System.Collections.Generic;

    public class CoroutineRepository
    {
        private readonly Dictionary<string, List<IEnumerator>> _dict = new();

        public bool TryGetCoroutines(string groupName, out List<IEnumerator> list) =>
            _dict.TryGetValue(groupName, out list);

        public void RemoveCoroutine(IEnumerator coroutine, string groupName) =>
            _dict[groupName].Remove(coroutine);

        public void RemoveAllCoroutines(string groupName) =>
            _dict[groupName].Clear();

        public void AddCoroutine(IEnumerator coroutine, string groupName)
        {
            if (!_dict.ContainsKey(groupName))
                _dict.Add(groupName, new List<IEnumerator>());

            _dict[groupName].Add(coroutine);
        }
    }
}