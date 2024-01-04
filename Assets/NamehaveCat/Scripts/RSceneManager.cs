using UnityEngine.SceneManagement;

namespace NamehaveCat.Scripts
{
    public static class RSceneManager
    {
        public static void Reload() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}