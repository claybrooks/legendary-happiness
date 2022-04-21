using committed.defaults.stacks;

namespace committed.defaults.history
{
    public class ConcurrentUnboundedCommitHistory : CommitHistory
    {
        public ConcurrentUnboundedCommitHistory() : base(new ConcurrentUnboundedActionStack(), new ConcurrentUnboundedActionStack())
        {

        }
    }
}