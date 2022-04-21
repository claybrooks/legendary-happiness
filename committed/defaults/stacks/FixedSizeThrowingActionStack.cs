namespace committed.defaults.stacks
{
    public class FixedSizeThrowingActionStack : FixedSizeActionStack
    {
        public FixedSizeThrowingActionStack(uint maxSize) : base(maxSize, true)
        {
        }
    }
}