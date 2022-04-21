namespace UndoRedo.defaults.stacks
{
    internal class FixedSizeThrowingActionStack : FixedSizeActionStack
    {
        public FixedSizeThrowingActionStack(uint maxSize) : base(maxSize, true)
        {
        }
    }
}