using System.Collections.Generic;
using UndoRedo;

namespace UndoRedo.defaults.stacks
{
    internal class UnboundedActionStack : IUndoRedoActionStack
    {
        private readonly Stack<IUndoRedoAction> _stack = new Stack<IUndoRedoAction>();

        public void Clear()
        {
            _stack.Clear();
        }

        public IUndoRedoAction? Peek()
        {
            if (_stack.TryPeek(out var action))
            {
                return action;
            }
            return null;
        }

        public void Push(IUndoRedoAction action)
        {
            _stack.Push(action);
        }

        public bool TryPop(out IUndoRedoAction action)
        {
            return _stack.TryPop(out action!);
        }
    }
}