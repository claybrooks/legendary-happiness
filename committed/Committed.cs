namespace committed
{
    public interface IAction
    {
        void Do();
        void Undo();
    }

    public interface ICommited
    {
        public void Commit(IAction action);
        public void Undo();
        public void Redo();
        public IAction? Current { get; }

    }

    public class Committed : ICommited
    {
        private readonly Stack<IAction> _undo = new Stack<IAction>();
        private readonly Stack<IAction> _redo = new Stack<IAction>();

        public IAction? Current => _undo.Peek();

        public void Commit(IAction action)
        {
            _redo.Clear();
            _undo.Push(action);
            action.Do();
        }

        public void Undo()
        {
            if (!_undo.TryPop(out var action))
            {
                return;
            }

            action.Undo();
            _redo.Push(action);
        }

        public void Redo()
        {
            if (!_redo.TryPop(out var action))
            {
                return;
            }

            action.Do();
            _undo.Push(action);
        }

    }
}