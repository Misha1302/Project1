namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Entities.LongRangeBullets;
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Snowball snowball;
        [SerializeField] private Vector2 direction = Vector2.one;
        [SerializeField] private float delay;

        private void Start()
        {
            GameManager.Instance.CoroutineManager.StartCoroutine(
                CoroutineManager.RepeatCoroutine(() =>
                {
                    var clone = Instantiate(snowball);
                    clone.Set(transform.position, direction);
                }, delay)
            );
        }
    }
}