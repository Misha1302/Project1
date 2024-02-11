namespace NamehaveCat.Scripts
{
    using UnityEngine;

    public class FallDeathShadower : MonoBehaviour
    {
        
        [SerializeField] private float multiplier = 0.1f;
        [SerializeField] private float boundY = -25;
        [SerializeField] private DeathPanel deathPanel;

        private void Update()
        {
            var delta = transform.position.y - boundY;
            if (delta >= 0) return;

            deathPanel.Alpha = -delta * multiplier;
        }

        private void OnDrawGizmos()
        {
            Debug.DrawLine(new Vector3(-1000, boundY), new Vector3(1000, boundY), Color.red);
        }

        private void OnValidate()
        {
            if (deathPanel == null)
                deathPanel = FindObjectOfType<DeathPanel>();
        }
    }
}