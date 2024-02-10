namespace NamehaveCat.Scripts.Machinery
{
    using UnityEngine;

    public class FlyingBlock : MonoBehaviour
    {
        [SerializeField] private ElectricitySource electricitySource;

        private void Update()
        {
            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(electricitySource.HasElectricity);
        }
    }
}