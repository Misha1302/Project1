namespace NamehaveCat.Scripts.Lift
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(Collider2D))]
    public class OneSidePlatform : MonoBehaviour
    {
        private Collider2D _collider2D;

        private void Start()
        {
            _collider2D = GetComponent<Collider2D>();
        }

        private void Update()
        {
            var playerY = GameManager.Instance.PlayerController.transform.position.y;
            var playerSize = GameManager.Instance.PlayerCharacteristics.PlayerSize;

            if (transform.position.y < playerY - playerSize / 2 && !_collider2D.enabled)
                _collider2D.enabled = true;

            else if (transform.position.y > playerY && _collider2D.enabled)
                _collider2D.enabled = false;
        }
    }
}