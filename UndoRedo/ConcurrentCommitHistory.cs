using System;
using System.Collections.Generic;

namespace UndoRedo
{
    public class ConcurrentCommitHistory : CommitHistory
    {
        private readonly object _lock = new object();

        public ConcurrentCommitHistory(IUndoRedoActionStack undo, IUndoRedoActionStack redo) : base (undo, redo)
        {

        }

        public override void Commit(IUndoRedoAction action)
        {
            lock (_lock)
            {
                base.Commit(action);
            }
        }

        public override void Commit(IEnumerable<IUndoRedoAction> actions)
        {
            lock (_lock)
            {
                base.Commit(actions);
            }
        }

        public override void Commit(Action @do, Action undo)
        {
            lock (_lock)
            {
                base.Commit(@do, undo);
            }
        }

        public override void Commit(IEnumerable<(Action @do, Action undo)> actions)
        {
            lock (_lock)
            {
                base.Commit(actions);
            }
        }

        public override void Undo()
        {
            lock (_lock)
            {
                base.Undo();
            }
        }

        public override void Redo()
        {
            lock (_lock)
            {
                base.Redo();
            }
        }

        public override void Clear()
        {
            lock (_lock)
            {
                base.Clear();
            }
        }
    }
}