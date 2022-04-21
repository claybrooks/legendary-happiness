namespace UndoRedo.defaults.stacks
{
    internal class FixedSizeRollingActionStack : FixedSizeActionStack
    {
        public FixedSizeRollingActionStack(uint maxSize) : base(maxSize, false)
        {
        }
    }
}