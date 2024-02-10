namespace NamehaveCat.Scripts.MImplementations
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MRbState
    {
        public RigidbodyConstraints2D constraints;
        public float mass;
        public bool needToBeRestored;
        public Rigidbody2D rigidbody2D;
        public Vector2 vel;
    }
}