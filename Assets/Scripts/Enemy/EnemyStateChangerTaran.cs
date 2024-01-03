namespace Enemy
{
    using System.Linq;
    using UnityEngine;

    public class EnemyStateChangerTaran : EnemyStateChanger
    {
        [SerializeField] private float distance;

        public override EnemyState TryGetNewState(Direction dir)
        {
            // if raycasted to player
            var direction = (dir & Direction.Left) != 0 ? Vector3.left : Vector3.right;
            var startPos = transform.position;
            startPos.y -= 0.5f;

            var hits = Physics2D.RaycastAll(startPos, direction, distance);
            var isPlayer = hits.Any(x => x.transform.TryGetComponent<PlayerTag>(out _));

            print(string.Join(", ", hits.Select(x => x.transform.name)));

            // ReSharper disable Unity.InefficientPropertyAccess
            Debug.DrawLine(startPos, startPos + direction * distance, Color.red);

            return isPlayer ? EnemyState.Attack : EnemyState.Walk;
        }
    }
}