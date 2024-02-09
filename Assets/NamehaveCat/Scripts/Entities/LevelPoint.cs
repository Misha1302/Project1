namespace NamehaveCat.Scripts.Entities
{
    using NamehaveCat.Scripts.MImplementations;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class LevelPoint : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<PlayerTag>(out _))
                MSceneManager.LoadNext();
        }
    }
}