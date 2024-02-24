namespace NamehaveCat.Scripts.Entities.Player
{
    using Cinemachine.Utility;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    public class PlayerStartPositioner : MonoBehaviour
    {
        private void Start()
        {
            var pos = GameDynamicData.SpawnPosition.get();

            if (!pos.IsNaN())
                GameManager.Instance.PlayerController.Rb2D.position = pos;
        }
    }
}