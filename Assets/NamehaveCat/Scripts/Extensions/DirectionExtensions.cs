namespace NamehaveCat.Scripts.Extensions
{
    using NamehaveCat.Scripts.Different;

    public static class DirectionExtensions
    {
        public static bool Has(this Direction value, Direction flag) =>
            (value & flag) != 0;
    }
}