namespace committed.defaults.stacks
{
    public class FixedSizeActionStack : IActionStack
    {
        private readonly LinkedList<IAction> _stack = new LinkedList<IAction>();
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

        public IAction? Peek()
        {
            return _stack.Last?.Value;
        }

        public void Push(IAction action)
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

        public bool TryPop(out IAction action)
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