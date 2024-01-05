namespace NamehaveCat.Scripts.Direction
{
    using System;

    [Flags]
    public enum Direction
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Up = 1 << 2,
    }
}