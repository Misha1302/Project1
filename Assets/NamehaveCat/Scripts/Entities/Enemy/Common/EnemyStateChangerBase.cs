namespace NamehaveCat.Scripts.Entities.Enemy.Common
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public abstract class EnemyStateChangerBase : MonoBehaviour
    {
        public abstract EnemyState TryGetNewState(Direction dir);
    }
}