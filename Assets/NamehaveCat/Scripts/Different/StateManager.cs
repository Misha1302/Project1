namespace NamehaveCat.Scripts.Different
{
    public static class StateManager
    {
        public static bool CanPause => !GameManager.Instance.PlayerDeath.IsDying;
        public static bool IsFlying => !GameManager.Instance.PlayerController.GroundChecker.IsGrounded;
    }
}