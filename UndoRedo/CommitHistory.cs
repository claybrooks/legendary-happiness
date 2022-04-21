using System;
using System.Collections.Generic;
using System.Linq;

namespace UndoRedo
{
    public class CommitHistory : IUndoRedo
    {
        private readonly IUndoRedoActionStack _undo;
        private readonly IUndoRedoActionStack _redo;

        public CommitHistory(IUndoRedoActionStack undo, IUndoRedoActionStack redo)
        {
            _undo = undo;
            _redo = redo;
        }

        public virtual void Commit(IUndoRedoAction action)
        {
            _redo.Clear();
            _undo.Push(action);
            action.Do();
        }

        public virtual void Commit(IEnumerable<IUndoRedoAction> actions)
        {
            Commit(new ChainedActions(actions));
        }

        public virtual void Commit(Action @do, Action undo)
        {
            Commit(new CallbackAction(@do, undo));
        }

        public virtual void Commit(IEnumerable<(Action @do, Action undo)> actions)
        {
            Commit(actions.Select(action => new CallbackAction(action.@do, action.undo)));
        }

        public virtual void Undo()
        {
            if (!_undo.TryPop(out var action))
            {
                return;
            }

            action.Undo();
            _redo.Push(action);
        }

        public virtual void Redo()
        {
            if (!_redo.TryPop(out var action))
            {
                return;
            }

            action.Do();
            _undo.Push(action);
        }

        public virtual void Clear()
        {
            _undo.Clear();
            _redo.Clear();
        }
    }
}