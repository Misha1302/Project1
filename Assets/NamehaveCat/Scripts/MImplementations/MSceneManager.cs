namespace NamehaveCat.Scripts.MImplementations
{
    using UnityEngine.SceneManagement;

    public static class MSceneManager
    {
        public static void Reload() =>
            LoadNext(0);

        public static void LoadNext(int offset = 1) =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + offset);
    }
}