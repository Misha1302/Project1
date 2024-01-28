namespace NamehaveCat.Scripts.Different
{
    using UnityEngine;

    public static class LayersManager
    {
        public static readonly LayerMask Enemy;
        public static readonly LayerMask ExceptNotAGround;
        public static readonly LayerMask ExceptEnemy;

        static LayersManager()
        {
            Enemy = Mask("Enemy");
            ExceptNotAGround = ExceptMask("NotAGround");
            ExceptEnemy = ExceptMask("Enemy");
        }

        private static LayerMask Mask(params string[] names) =>
            LayerMask.GetMask(names);

        private static LayerMask ExceptMask(params string[] names) =>
            ~LayerMask.GetMask(names);
    }
}