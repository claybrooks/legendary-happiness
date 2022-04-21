using committed.stacks;

namespace committed.defaults
{
    public class FixedSizeRollingCommitHistory : CommitHistory
    {
        // The redo stack can never be larger than the undo stack, therefore only the undo stack needs to be unbounded
        public FixedSizeRollingCommitHistory(uint maxSize) : base(new FixedSizeRollingActionStack(maxSize), new UnboundedActionStack())
        {

        }
    }
}
