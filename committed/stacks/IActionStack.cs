namespace committed.stacks
{
    public interface IActionStack
    {
        void Push(IAction action);
        bool TryPop(out IAction action);
        IAction Pop();
        IAction Peek();
        void Clear();
    }
}
