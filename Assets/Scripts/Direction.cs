using System;

[Flags]
public enum Direction
{
    Left = 1 << 0,
    Right = 1 << 1,
    Up = 1 << 2
}