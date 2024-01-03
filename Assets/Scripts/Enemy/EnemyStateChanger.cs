namespace Enemy
{
    using UnityEngine;

    public abstract class EnemyStateChanger : MonoBehaviour
    {
        public abstract EnemyState TryGetNewState(Direction dir);
    }
}