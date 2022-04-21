using committed.defaults.stacks;

namespace committed.defaults.history
{
    public class UnboundedCommitHistory : CommitHistory
    {
        public UnboundedCommitHistory() : base(new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }
}