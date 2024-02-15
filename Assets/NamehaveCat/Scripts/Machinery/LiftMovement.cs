namespace NamehaveCat.Scripts.Machinery
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    [RequireComponent(typeof(StickyPlatform))]
    public class LiftMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Transform top;
        [SerializeField] private Transform bottom;

        private void FixedUpdate()
        {
            var sin = Mathf.Sin(GameManager.Instance.Time.CurTime * speed) / 2 + 0.5f;
            transform.position = Vector2.Lerp(top.position, bottom.position, sin);
        }
    }
}