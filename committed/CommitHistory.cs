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

        public IAction? Current => _undo.Peek();

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
    }
    
    public class UnboundedCommitHistory : CommitHistory
    {
        public UnboundedCommitHistory() : base (new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }

    public class ConcurrentUnboundedCommitHistory : CommitHistory
    {
        public ConcurrentUnboundedCommitHistory() : base(new ConcurrentUnboundedActionStack(), new ConcurrentUnboundedActionStack())
        {

        }
    }

    public class FixedSizeRollingCommitHistory : CommitHistory
    {
        // The redo stack can never be larger than the undo stack, therefore only the undo stack needs to be unbounded
        public FixedSizeRollingCommitHistory(uint maxSize) : base(new FixedSizeRollingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }

    public class FixedSizeThrowingCommitHistory : CommitHistory
    {
        // The redo stack can never be larger than the undo stack, therefore only the undo stack needs to be unbounded
        public FixedSizeThrowingCommitHistory(uint maxSize) : base(new FixedSizeThrowingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}