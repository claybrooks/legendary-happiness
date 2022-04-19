namespace committed
{
    public interface IAction
    {
        void Do();
        void Undo();
    }

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
            foreach (var action in _actions)
            {
                action.Undo();
            }
        }
    }

    public interface ICommitted
    {
        public void Commit(IAction action);
        public void Commit(IEnumerable<IAction> actions);
        public void Undo();
        public void Redo();
        public IAction? Current { get; }

    }

    public class Committed : ICommitted
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

        public void Commit(IEnumerable<IAction> actions)
        {
            Commit(new ChainedActions(actions));
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