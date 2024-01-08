namespace NamehaveCat.Scripts.Velocipedi
{
    using System;
    using NamehaveCat.Scripts.Different;

    public class ExecuteInNextFrame : MonoBehSingleton<ExecuteInNextFrame>
    {
        private readonly Action[] _funcs = new Action[1024];
        private int _len;

        private void Update()
        {
            for (var i = 0; i < _len; i++)
            {
                _funcs[i]();
                _funcs[i] = null;
            }

            _len = 0;
        }

        public void Execute(Action func)
        {
            if (_len < 0 || _len >= _funcs.Length)
                Thrower.Throw(new IndexOutOfRangeException("Too many functions to execute in the next frame"));

            _funcs[_len] = func;
            _len++;
        }
    }
}