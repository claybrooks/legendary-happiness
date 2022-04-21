namespace committed.defaults.stacks
{
    public class UnboundedActionStack : IActionStack
    {
        private readonly Stack<IAction> _stack = new Stack<IAction>();

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