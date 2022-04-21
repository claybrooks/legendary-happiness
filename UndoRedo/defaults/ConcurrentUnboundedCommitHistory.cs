using UndoRedo.defaults.stacks;

namespace UndoRedo.defaults
{
    public class ConcurrentUnboundedCommitHistory : CommitHistory
    {
        public ConcurrentUnboundedCommitHistory() : base(new ConcurrentUnboundedActionStack(), new ConcurrentUnboundedActionStack())
        {

        }
    }
}