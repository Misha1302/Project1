namespace NamehaveCat.Scripts.Machinery
{
    using UnityEngine;

    public abstract class ElectricitySource : MonoBehaviour
    {
        public bool HasElectricity { get; protected set; } = false;
    }
}