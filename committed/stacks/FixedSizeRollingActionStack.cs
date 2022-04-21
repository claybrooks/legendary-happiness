namespace committed.stacks
{
    public class FixedSizeRollingActionStack : FixedSizeActionStack
    {
        public FixedSizeRollingActionStack(uint maxSize) : base(maxSize, false)
        {
        }
    }
}
