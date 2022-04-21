namespace UndoRedo
{
    public interface IAction
    {
        /// <summary>
        /// Called during commit and redo
        /// </summary>
        void Do();

        /// <summary>
        /// Called during undo
        /// </summary>
        void Undo();
    }
}