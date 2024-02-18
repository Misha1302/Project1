namespace NamehaveCat.Scripts
{
    using NamehaveCat.Scripts.Helpers;
    using NamehaveCat.Scripts.Tags;
    using UnityEngine;

    public static class Validator
    {
        // ReSharper disable once Unity.InefficientPropertyAccess
        public static bool ValidateEnemyCollision(Component tr) =>
            !tr.TryGetComponent<PlayerTag>(out _) &&
            tr.gameObject.layer != LayersManager.NotAGround &&
            tr.gameObject.layer != LayersManager.IgnoreRaycast;
    }
}