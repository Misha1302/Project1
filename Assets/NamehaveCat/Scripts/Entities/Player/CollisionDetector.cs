namespace NamehaveCat.Scripts.Entities.Player
{
    using NamehaveCat.Scripts.Extensions;
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private float rayLen = 0.7f;

        private readonly RaycastHit2D[] _results = new RaycastHit2D[GameData.MaxCollidersCount];

        public bool HasObjectOnRight() => HasObjectOnDir(Vector2.right);

        public bool HasObjectOnLeft() => HasObjectOnDir(Vector2.left);

        private bool HasObjectOnDir(Vector2 direction)
        {
            DrawLine(direction);

            var len = Physics2D.RaycastNonAlloc(transform.position, direction, _results, rayLen);
            return _results.Any(
                x => !x.transform.TryGetComponent<PlayerTag>(out _) &&
                     !x.transform.TryGetComponent<MovableTag>(out _) &&
                     !x.collider.isTrigger,
                len
            );
        }

        private void DrawLine(Vector2 direction)
        {
            // ReSharper disable Unity.InefficientPropertyAccess
            Debug.DrawLine(
                transform.position,
                transform.position + (Vector3)(direction.normalized * rayLen),
                Color.red
            );
        }
    }
}