using UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UndoRedo
{
    public class CommitHistory : ICommitted
    {
        private readonly IActionStack _undo;
        private readonly IActionStack _redo;

        public CommitHistory(IActionStack undo, IActionStack redo)
        {
            _undo = undo;
            _redo = redo;
        }

        public void Commit(IAction action)
        {
            _redo.Clear();
            _undo.Push(action);
            action.Do();
        }

        public void Commit(IEnumerable<IAction> actions)
        {
            Commit(new ChainedActions(actions));
        }

        public void Commit(Action @do, Action undo)
        {
            Commit(new CallbackAction(@do, undo));
        }

        public void Commit(IEnumerable<(Action @do, Action undo)> actions)
        {
            Commit(actions.Select(action => new CallbackAction(action.@do, action.undo)));
        }

        public void Undo()
        {
            if (!_undo.TryPop(out var action))
            {
                return;
            }

            action.Undo();
            _redo.Push(action);
        }

        public void Redo()
        {
            if (!_redo.TryPop(out var action))
            {
                return;
            }

            action.Do();
            _undo.Push(action);
        }

        public void Clear()
        {
            _undo.Clear();
            _redo.Clear();
        }
    }
}