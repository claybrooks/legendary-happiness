using System;
using System.Collections.Generic;

namespace UndoRedo
{
    public interface IUndoRedo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        void Commit(IUndoRedoAction action);

        /// <summary>
        /// <paramref name="do"/> is invoked during the commit
        /// </summary>
        /// <param name="action"></param>
        void Commit(Action @do, Action undo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>All committed actions will be invoked upon a single call of Undo/Redo</remarks>
        void Commit(IEnumerable<IUndoRedoAction> actions);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <remarks>All committed actions will be invoked upon a single call of Undo/Redo</remarks>
        void Commit(IEnumerable<(Action @do, Action undo)> actions);

        /// <summary>
        /// Invokes the previously committed action
        /// </summary>
        /// <param name="action"></param>
        void Undo();

        /// <summary>
        /// Invokes the most recently undone action
        /// </summary>
        void Redo();

        /// <summary>
        /// Clears all commit history
        /// </summary>
        void Clear();
    }
}