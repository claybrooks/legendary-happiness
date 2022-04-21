using UndoRedo.defaults.stacks;

namespace UndoRedo.defaults
{
    public class UnboundedCommitHistory : CommitHistory
    {
        public UnboundedCommitHistory() : base(new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }
}