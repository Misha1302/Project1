using UnityEngine.SceneManagement;

public static class RSceneManager
{
    public static void Reload() =>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}