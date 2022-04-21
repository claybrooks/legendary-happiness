namespace committed.defaults.stacks
{
    public class FixedSizeRollingActionStack : FixedSizeActionStack
    {
        public FixedSizeRollingActionStack(uint maxSize) : base(maxSize, false)
        {
        }
    }
}