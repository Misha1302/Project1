namespace NamehaveCat.Scripts.Different
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class LevelPoint : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<PlayerTag>(out _))
                RSceneManager.LoadNext();
        }
    }
}