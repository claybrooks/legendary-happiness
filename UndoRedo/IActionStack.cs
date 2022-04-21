namespace UndoRedo
{
    public interface IActionStack
    {
        /// <summary>
        /// Pushes <paramref name="action"/> onto internal stack implementation
        /// </summary>
        /// <param name="action"></param>
        void Push(IAction action);

        /// <summary>
        /// Tries to pop the next item from the stack.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>True if there was an action to pop.  If False, action is null.  If True, action is the top of the stack</returns>
        bool TryPop(out IAction action);

        /// <summary>
        /// Clears the stack
        /// </summary>
        void Clear();
    }
}