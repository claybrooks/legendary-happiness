using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndoRedo;

namespace test.application.actions
{
    class ShowBoxInfoAction : IAction
    {
        private readonly BoxUIDisplay _display;

        public ShowBoxInfoAction(BoxUIDisplay display)
        {
            _display = display;
        }

        public void Do()
        {
            _display.Show();
        }

        public void Undo()
        {
            _display.Hide();
        }
    }

    class HideBoxInfoAction : IAction
    {
        private readonly BoxUIDisplay _display;

        public HideBoxInfoAction(BoxUIDisplay display)
        {
            _display = display;
        }

        public void Do()
        {
            _display.Hide();
        }

        public void Undo()
        {
            _display.Show();
        }
    }

    class MoveBoxInfoAction : IAction
    {
        private readonly BoxUIDisplay _display;
        private readonly Transform2D _newPosition;
        private readonly Transform2D _oldPosition;

        public MoveBoxInfoAction(BoxUIDisplay display, Transform2D oldPosition, Transform2D newPosition)
        {
            _display = display;
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        public void Do()
        {
            _display.Move(_newPosition);
        }

        public void Undo()
        {
            _display.Move(_oldPosition);
        }
    }
}
