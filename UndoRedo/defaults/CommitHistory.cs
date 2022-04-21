namespace UndoRedo.defaults
{
    public class UndoRedo : FixedSizeRollingCommitHistory
    {
        public UndoRedo(uint size) : base(size)
        {

        }
    }
}
