using UndoRedo.defaults.stacks;

namespace UndoRedo.defaults
{
    public class FixedSizeThrowingCommitHistory : CommitHistory
    {
        public FixedSizeThrowingCommitHistory(uint maxSize) : base(new FixedSizeThrowingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }

    public class ConcurrentFixedSizeThrowingCommitHistory : ConcurrentCommitHistory
    {
        public ConcurrentFixedSizeThrowingCommitHistory(uint maxSize) : base(new FixedSizeThrowingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}