using UndoRedo.defaults.stacks;

namespace UndoRedo.defaults
{
    public class UnboundedCommitHistory : CommitHistory
    {
        public UnboundedCommitHistory() : base(new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }

    public class ConcurrentUnboundedCommitHistory : ConcurrentCommitHistory
    {
        public ConcurrentUnboundedCommitHistory() : base(new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }
}