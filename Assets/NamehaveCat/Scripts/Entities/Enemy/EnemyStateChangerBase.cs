namespace NamehaveCat.Scripts.Entities.Enemy
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public abstract class EnemyStateChangerBase : MonoBehaviour
    {
        public abstract EnemyState TryGetNewState(Direction dir);
    }
}