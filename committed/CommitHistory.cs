using committed.stacks;

namespace committed
{
    public class CommitHistory : ICommitted
    {
        private readonly IActionStack _undo;
        private readonly IActionStack _redo;

        public CommitHistory(IActionStack undo, IActionStack redo)
        {
            _undo = undo;
            _redo = redo;
        }

        public virtual void Commit(IAction action)
        {
            _redo.Clear();
            _undo.Push(action);
            action.Do();
        }

        public virtual void Commit(IEnumerable<IAction> actions)
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

        public void Clear()
        {
            _undo.Clear();
            _redo.Clear();
        }
    }
}