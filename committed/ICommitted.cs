namespace committed
{
    public interface ICommitted
    {
        /// <summary>
        /// <paramref name="action"/> is invoked during the commit
        /// </summary>
        /// <param name="action"></param>
        public void Commit(IAction action);

        /// <summary>
        /// <paramref name="do"/> is invoked during the commit
        /// </summary>
        /// <param name="action"></param>
        public void Commit(Action @do, Action undo);

        /// <summary>
        /// <paramref name="actions"/> are invoked during the commit
        /// </summary>
        /// <param name="action"></param>
        public void Commit(IEnumerable<IAction> actions);

        /// <summary>
        /// <paramref name="action"/> is invoked during the commit
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>The first item in the tuple is Do(), the second item is Undo()</remarks>
        public void Commit(IEnumerable<Tuple<Action, Action>> actions);

        /// <summary>
        /// Invokes the previously committed action
        /// </summary>
        /// <param name="action"></param>
        public void Undo();

        /// <summary>
        /// Invokes the most recently undone action
        /// </summary>
        public void Redo();

        /// <summary>
        /// Clears all commit history
        /// </summary>
        public void Clear();
    }
}