namespace NamehaveCat.Scripts.Enemy
{
    using System;

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
            _funcs[_len] = func;
            _len++;
        }
    }
}