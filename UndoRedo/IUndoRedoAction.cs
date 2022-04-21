namespace UndoRedo
{
    public interface IUndoRedoAction
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