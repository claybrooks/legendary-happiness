using committed;
using test.application.actions;

namespace test.application
{
    public struct Transform2D
    {
        public float X = 0;
        public float Y = 0;

        public Transform2D(float x, float y) { X = x; Y = y; }
    }

    public class Box : IDisposable
    {
        public Transform2D Transform2D;
        public bool Deleted { get; private set; }

        public Box(Transform2D transform2D)
        {
            Transform2D = transform2D;
            Deleted = false;
        }

        public void Dispose()
        {
            Transform2D.X = -1;
            Transform2D.Y = -1;
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

        public string Info => $"X:{_boxWrapper.Object.Transform2D.X}, Y:{_boxWrapper.Object.Transform2D.Y}";
    }

    public class BoxManipulator
    {
        private readonly ICommitted _committed;
        private ObjectWrapper<Box> _boxWrapper;

        public BoxManipulator(ICommitted committed, ObjectWrapper<Box> boxWrapper)
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
        private readonly ICommitted _committed;
        private readonly BoxUIDisplay _display;

        public BoxUIDisplayManipulator(ICommitted committed, BoxUIDisplay display)
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
        private readonly ICommitted _committed;

        public BoxElement(ICommitted committed, Box box)
        {
            _wrapper = new ObjectWrapper<Box>(box);
            _display = new BoxUIDisplay(_wrapper);
            _boxManipulator = new BoxManipulator(committed, _wrapper);
            _displayManipulator = new BoxUIDisplayManipulator(committed, _display);
            _committed = committed;

            // To preserve creation, push an action now
            committed.Commit(new CreateBoxAction(_wrapper, box.Transform2D.X, box.Transform2D.Y));
        }

        public void Move(Transform2D position)
        {
            if (!_wrapper.Object.Deleted)
            {
                _committed.Commit(new List<IAction>
                {
                    new MoveBoxAction(_wrapper, position),
                    new MoveBoxInfoAction(_display, _wrapper.Object.Transform2D, new Transform2D(position.X, position.Y < 5 ? 0 : position.Y - 5))
                });
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
                return $"Allocated:{!box.Deleted}, X:{box.Transform2D.X}, Y:{box.Transform2D.Y}, Info Visible:{_display.IsVisible}, Info X:{_display.Position.X}, Info Y:{_display.Position.Y}";
            }
        }
    }
}
