namespace NamehaveCat.Scripts.MImplementations
{
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException(string s) : base(s)
        {
        }
    }
}