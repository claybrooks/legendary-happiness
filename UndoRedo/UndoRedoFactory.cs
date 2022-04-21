namespace UndoRedo
{
    public static class UndoRedoFactory
    {
        public static IUndoRedo Create(int maxSize = -1, bool shouldBeThreadSafe = false)
        {
            if (shouldBeThreadSafe)
            {
                if (maxSize > 0)
                {
                    return new defaults.ConcurrentFixedSizeRollingCommitHistory((uint)maxSize);
                }
                else
                {
                    return new defaults.ConcurrentUnboundedCommitHistory();
                }
            }
            else
            {
                if (maxSize > 0)
                {
                    return new defaults.FixedSizeRollingCommitHistory((uint)maxSize);
                }
                else
                {
                    return new defaults.UnboundedCommitHistory();
                }
            }
        }
    }
}
