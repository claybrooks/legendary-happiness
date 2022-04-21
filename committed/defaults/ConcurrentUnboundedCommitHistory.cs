using committed.stacks;

namespace committed.defaults
{
    public class ConcurrentUnboundedCommitHistory : CommitHistory
    {
        public ConcurrentUnboundedCommitHistory() : base(new ConcurrentUnboundedActionStack(), new ConcurrentUnboundedActionStack())
        {

        }
    }
}
