using System.Collections.Concurrent;
using UndoRedo;

namespace UndoRedo.defaults.stacks
{
    internal class ConcurrentUnboundedActionStack : IActionStack
    {
        private readonly ConcurrentStack<IAction> _stack = new ConcurrentStack<IAction>();

        public void Clear()
        {
            _stack.Clear();
        }

        public IAction? Peek()
        {
            if (_stack.TryPeek(out var action))
            {
                return action;
            }
            return null;
        }

        public void Push(IAction action)
        {
            _stack.Push(action);
        }

        public bool TryPop(out IAction action)
        {
            return _stack.TryPop(out action!);
        }
    }
}