namespace NamehaveCat.Scripts
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartSceneManager : MonoBehaviour
    {
        private static int _sceneNeedToBeLoaded = -1;

        private void OnEnable()
        {
            var index = SceneManager.GetActiveScene().buildIndex;
            if (index == 0 || _sceneNeedToBeLoaded == int.MinValue) return;

            _sceneNeedToBeLoaded = index;

            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += (scene, _) =>
            {
                if (scene.buildIndex != 0 || _sceneNeedToBeLoaded == int.MinValue)
                    return;

                SceneManager.LoadScene(_sceneNeedToBeLoaded);
                _sceneNeedToBeLoaded = int.MinValue;
            };
        }
    }
}