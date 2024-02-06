namespace NamehaveCat.Scripts.Helpers
{
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public class CameraScreen : MonoBehaviour
    {
        [SerializeField] private Camera cameraCopy;

        private Camera _thisCamera;

        private void Start()
        {
            _thisCamera = GetComponent<Camera>();
            _thisCamera.tag = "Untagged";
            _thisCamera.enabled = false;
            _thisCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        }

        private void OnValidate()
        {
            if (cameraCopy == null)
                cameraCopy = Camera.main;
        }

        public RenderTexture TakeScreen()
        {
            CopyCameraState();
            Render();
            return _thisCamera.targetTexture;
        }

        private void Render()
        {
            _thisCamera.Render();
        }

        private void CopyCameraState()
        {
            // ReSharper disable Unity.InefficientPropertyAccess

            _thisCamera.cameraType = cameraCopy.cameraType;
            _thisCamera.backgroundColor = cameraCopy.backgroundColor;
            _thisCamera.orthographic = cameraCopy.orthographic;
            _thisCamera.orthographicSize = cameraCopy.orthographicSize;
            _thisCamera.depth = cameraCopy.depth;
            _thisCamera.transform.position = cameraCopy.transform.position;
            _thisCamera.transform.rotation = cameraCopy.transform.rotation;
        }
    }
}