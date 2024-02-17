namespace NamehaveCat.Scripts.MImplementations
{
    using System;

    [Flags]
    public enum MRbConstraints2D
    {
        LeftX = 1 << 0,
        RightX = 1 << 1,
        UpY = 1 << 2,
        DownY = 1 << 3,
        All = int.MaxValue
    }
}