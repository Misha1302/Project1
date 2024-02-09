namespace NamehaveCat.Scripts.Helpers
{
    using System.Linq;
    using UnityEngine;

    public static class LayersManager
    {
        public static readonly LayerMask Enemy;
        public static readonly LayerMask ExceptNotAGround;
        public static readonly LayerMask ExceptEnemy;
        public static readonly LayerMask NotAGround;
        public static readonly LayerMask IgnoreRaycast;

        static LayersManager()
        {
            Enemy = Mask("Enemy");
            NotAGround = Mask("NotAGround");
            IgnoreRaycast = Mask("Ignore Raycast");
            ExceptNotAGround = ExceptMask("NotAGround");
            ExceptEnemy = ExceptMask("Enemy");
        }

        private static LayerMask Mask(params string[] names) =>
            LayerMask.GetMask(names);

        private static LayerMask ExceptMask(params string[] names) =>
            ~LayerMask.GetMask(names.Append("Ignore Raycast").ToArray());
    }
}