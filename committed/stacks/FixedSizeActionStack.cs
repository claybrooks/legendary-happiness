namespace committed.stacks
{
    public class FixedSizeActionStack : IActionStack
    {
        private readonly LinkedList<IAction> _stack = new LinkedList<IAction>();
        private readonly uint _fixedSize;

        public FixedSizeActionStack(uint maxSize)
        {
            _fixedSize = maxSize;
        }

        public void Clear()
        {
            _stack.Clear();
        }

        public IAction Peek()
        {
            return _stack.First();
        }

        public IAction Pop()
        {
            var action = _stack.First();
            _stack.RemoveFirst();
            return action;
        }

        public void Push(IAction action)
        {
            _stack.AddFirst(action);
        }

        public bool TryPop(out IAction action)
        {
            action = null!;
            if (_stack.Count() == 0)
            {
                return false;
            }
            action = Pop();
            return true;
        }
    }
}
