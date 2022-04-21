using System;
using System.Collections.Generic;

namespace UndoRedo.defaults.stacks
{
    internal class FixedSizeActionStack : IUndoRedoActionStack
    {
        private readonly LinkedList<IUndoRedoAction> _stack = new LinkedList<IUndoRedoAction>();
        private readonly uint _fixedSize;
        private readonly bool _throwWhenFull;

        public FixedSizeActionStack(uint maxSize, bool throwWhenFull)
        {
            _fixedSize = maxSize;
            _throwWhenFull = throwWhenFull;
        }

        public void Clear()
        {
            _stack.Clear();
        }

        public IUndoRedoAction? Peek()
        {
            return _stack.Last?.Value;
        }

        public void Push(IUndoRedoAction action)
        {
            if (_stack.Count == _fixedSize)
            {
                if (_throwWhenFull)
                {
                    throw new InvalidOperationException();
                }
                _stack.RemoveFirst();
            }
            _stack.AddLast(action);
        }

        public bool TryPop(out IUndoRedoAction action)
        {
            action = null!;

            if (_stack.Count == 0)
            {
                return false;
            }

            action = _stack.Last?.Value!;
            _stack.RemoveLast();
            return true;
        }
    }
}