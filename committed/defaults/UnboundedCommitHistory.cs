using committed.stacks;

namespace committed.defaults
{
    public class UnboundedCommitHistory : CommitHistory
    {
        public UnboundedCommitHistory() : base(new UnboundedActionStack(), new UnboundedActionStack())
        {

        }
    }
}
