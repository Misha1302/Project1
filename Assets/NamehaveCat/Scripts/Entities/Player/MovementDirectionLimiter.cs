namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Extensions;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionDetector))]
    public class MovementDirectionLimiter : MonoBehaviour
    {
        private CollisionDetector _collisionDetector;
        private Rigidbody2D _rb2D;

        private void Start()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _collisionDetector = GetComponent<CollisionDetector>();

            GameManager.Instance.InputController.onPress.AddListener(_ => LimitHorizontal());
        }

        private void LimitHorizontal()
        {
            if ((_rb2D.velocity.x < 0 && _collisionDetector.HasObjectOnLeft()) ||
                (_rb2D.velocity.x > 0 && _collisionDetector.HasObjectOnRight()))
                _rb2D.velocity = _rb2D.velocity.WithX(0);
        }
    }
}