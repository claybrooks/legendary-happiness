namespace committed
{
    public interface ICommitted
    {
        public void Commit(IAction action);
        public void Commit(IEnumerable<IAction> actions);
        public void Undo();
        public void Redo();
        public IAction? Current { get; }
    }
}
