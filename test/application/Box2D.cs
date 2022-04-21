using test.application.actions;
using UndoRedo;

namespace test.application
{
    public struct Transform2D
    {
        public int X = 0;
        public int Y = 0;

        public Transform2D(int x, int y) { X = x; Y = y; }
    }

    public class Box : IDisposable
    {
        public Transform2D Position;
        public bool Deleted { get; private set; }

        public ConsoleColor Color;

        public Box(Transform2D transform2D)
        {
            Position = transform2D;
            Deleted = false;
        }

        public void Dispose()
        {
            Position.X = -1;
            Position.Y = -1;
            Deleted = true;
        }
    }
    
    public class BoxUIDisplay
    {
        private ObjectWrapper<Box> _boxWrapper;

        public bool IsVisible { get; private set; }
        public Transform2D Position { get; private set; }

        public BoxUIDisplay(ObjectWrapper<Box> boxWrapper)
        {
            _boxWrapper = boxWrapper;
            IsVisible = false;
            Position = new Transform2D(1, 1);
        }


        public void Show()
        {
            IsVisible = true;
        }

        public void Hide()
        {
            IsVisible = false;
        }

        public void Move(Transform2D transform)
        {
            Position = transform;
        }

        public string Info => $"X:{_boxWrapper.Object.Position.X}, Y:{_boxWrapper.Object.Position.Y}";
    }

    public class BoxManipulator
    {
        private readonly IUndoRedo _committed;
        private ObjectWrapper<Box> _boxWrapper;

        public BoxManipulator(IUndoRedo committed, ObjectWrapper<Box> boxWrapper)
        {
            _committed = committed;
            _boxWrapper = boxWrapper;
        }


        public void Create()
        {
            if (_boxWrapper.Object.Deleted)
            {
                _committed.Commit(new CreateBoxAction(_boxWrapper, 1, 1));
            }
        }

        public void Move(Transform2D transform)
        {
            _committed.Commit(new MoveBoxAction(_boxWrapper, transform));
        }

        public void Delete()
        {
            _committed.Commit(new DeleteBoxAction(_boxWrapper));
        }
    }

    public class BoxUIDisplayManipulator
    {
        private readonly IUndoRedo _committed;
        private readonly BoxUIDisplay _display;

        public BoxUIDisplayManipulator(IUndoRedo committed, BoxUIDisplay display)
        {
            _committed = committed;
            _display = display;
        }


        public void Show()
        {
            if (!_display.IsVisible)
            {
                _committed.Commit(new ShowBoxInfoAction(_display));
            }
        }

        public void Hide()
        {
            if (_display.IsVisible)
            {
                _committed.Commit(new HideBoxInfoAction(_display));
            }
        }

        public void Move(Transform2D transform)
        {
            _committed.Commit(new MoveBoxInfoAction(_display, _display.Position, transform));
        }
    }

    public class BoxElement
    {
        private readonly ObjectWrapper<Box> _wrapper;
        private readonly BoxUIDisplay _display;
        private readonly BoxManipulator _boxManipulator;
        private readonly BoxUIDisplayManipulator _displayManipulator;
        private readonly IUndoRedo _committed;

        public BoxElement(IUndoRedo committed, Box box)
        {
            _wrapper = new ObjectWrapper<Box>(box);
            _display = new BoxUIDisplay(_wrapper);
            _boxManipulator = new BoxManipulator(committed, _wrapper);
            _displayManipulator = new BoxUIDisplayManipulator(committed, _display);
            _committed = committed;

            // To preserve creation, push an action now
            committed.Commit(new CreateBoxAction(_wrapper, box.Position.X, box.Position.Y));
        }

        public void Move(Transform2D position)
        {
            if (!_wrapper.Object.Deleted && position.X >= 0 && position.Y >= 0)
            {
                _committed.Commit(new List<IUndoRedoAction>
                {
                    new MoveBoxAction(_wrapper, position),
                    new MoveBoxInfoAction(_display, _wrapper.Object.Position, new Transform2D(position.X, position.Y < 5 ? 0 : position.Y - 5))
                });
            }
        }

        public void Create()
        {
            if (_wrapper.Object.Deleted)
            {
                _boxManipulator.Create();
            }
        }

        public void Delete()
        {
            if (!_wrapper.Object.Deleted)
            {
                _boxManipulator.Delete();
            }
        }

        public void ShowInfo()
        {
            if (!_wrapper.Object.Deleted)
            {
                _displayManipulator.Show();
            }
        }

        public void HideInfo()
        {
            if (!_wrapper.Object.Deleted)
            {
                _displayManipulator.Hide();
            }
        }

        public bool Deleted => _wrapper.Object.Deleted;

        public string Meta
        {
            get
            {
                var box = _wrapper.Object;
                return $"Allocated:{!box.Deleted}, X:{box.Position.X}, Y:{box.Position.Y}, Info Visible:{_display.IsVisible}, Info X:{_display.Position.X}, Info Y:{_display.Position.Y}";
            }
        }

        public Transform2D Position => _wrapper.Object.Position;

        public void MoveUpOne()
        {
            Move(new Transform2D(Position.X, Position.Y-1));
        }
        public void MoveDownOne()
        {
            Move(new Transform2D(Position.X, Position.Y + 1));
        }
        public void MoveLeftOne()
        {
            Move(new Transform2D(Position.X - 1, Position.Y));
        }
        public void MoveRightOne()
        {
            Move(new Transform2D(Position.X + 1, Position.Y));
        }
    }
}
