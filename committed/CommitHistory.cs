using committed.stacks;

namespace committed
{
    public class CommitHistory<U, R> : ICommitted
        where U : IActionStack
        where R : IActionStack
    {
        private readonly U _undo;
        private readonly R _redo;

        public CommitHistory(U undo, R redo)
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
    
    public class UnboundedCommitHistory : CommitHistory<UnboundedActionStack, UnboundedActionStack>
    {
        public UnboundedCommitHistory() : base (new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }

    public class FixedSizeCommitHistory : CommitHistory<FixedSizeActionStack, UnboundedActionStack>
    {
        // The redo stack can never be larger than the undo stack, therefore only the undo stack needs to be unbounded
        public FixedSizeCommitHistory(uint maxSize) : base(new FixedSizeActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}