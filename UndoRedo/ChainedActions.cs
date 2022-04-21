using System.Collections.Generic;
using System.Linq;

namespace UndoRedo
{
    public class ChainedActions : IAction
    {
        private readonly IEnumerable<IAction> _actions;

        public ChainedActions(IEnumerable<IAction> actions)
        {
            _actions = actions;
        }

        public void Do()
        {
            foreach (var action in _actions)
            {
                action.Do();
            }
        }

        public void Undo()
        {
            foreach (var action in _actions.Reverse())
            {
                action.Undo();
            }
        }
    }
}