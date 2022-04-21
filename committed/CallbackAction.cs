using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace committed
{
    internal class CallbackAction : IAction
    {
        private readonly Action _do;
        private readonly Action _undo;

        public CallbackAction(Action @do, Action undo)
        {
            _do = @do;
            _undo = undo;
        }

        public void Do()
        {
           _do();
        }

        public void Undo()
        {
            _undo();
        }
    }
}
