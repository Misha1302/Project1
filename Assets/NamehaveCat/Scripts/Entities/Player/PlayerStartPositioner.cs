namespace NamehaveCat.Scripts.Entities.Player
{
    using Cinemachine.Utility;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.Helpers;
    using UnityEngine;

    public class PlayerStartPositioner : MonoBehaviour
    {
        private void Start()
        {
            if (!GameData.startPosition.IsNaN())
                GameManager.Instance.PlayerController.Rb2D.position = GameData.startPosition;
        }
    }
}