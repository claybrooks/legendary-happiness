namespace committed.stacks
{
    public class UnboundedActionStack : IActionStack
    {
        private readonly Stack<IAction> _stack = new Stack<IAction>();

        public void Clear()
        {
            _stack.Clear();
        }

        public IAction Peek()
        {
            return _stack.Peek();
        }

        public IAction Pop()
        {
            return _stack.Pop();
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
