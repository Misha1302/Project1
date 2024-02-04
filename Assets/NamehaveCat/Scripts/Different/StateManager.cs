namespace NamehaveCat.Scripts.Different
{
    public static class StateManager
    {
        public static bool CanPause => !GameManager.Instance.PlayerDeath.IsDying;
    }
}