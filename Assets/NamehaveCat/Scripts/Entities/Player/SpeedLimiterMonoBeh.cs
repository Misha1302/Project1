namespace NamehaveCat.Scripts.Entities.Player
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CollisionDetector))]
    public class SpeedLimiterMonoBeh : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 4f;

        private void Start()
        {
            var speedLimiter = new SpeedLimiter(
                GetComponent<Rigidbody2D>(),
                GetComponent<CollisionDetector>(),
                maxSpeed
            );

            GameManager.Instance.InputController.onPress.AddListener(speedLimiter.LimitHorizontal);
        }
    }
}