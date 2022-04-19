using committed;

namespace test.application
{
    struct Transform2D
    {
        public float X = 0;
        public float Y = 0;

        public Transform2D(float x, float y) { X = x; Y = y; }
    }

    class Box : IDisposable
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

    class MoveBoxAction : IAction
    {
        private readonly Transform2D _current;
        private readonly Transform2D _new;
        private readonly ObjectWrapper<Box> _box;

        public MoveBoxAction(ObjectWrapper<Box> box, float x, float y)
        {
            _box = box;
            _current = box.Object.Transform2D;
            _new = new Transform2D(x, y);
        }

        public void Do()
        {
            _box.Object.Transform2D = _new;
        }

        public void Undo()
        {
            _box.Object.Transform2D = _current;
        }
    }

    class CreateBoxAction : IAction
    {
        private readonly Transform2D _position;
        public ObjectWrapper<Box>? BoxWrapper;

        public CreateBoxAction(float x, float y)
        {
            _position = new Transform2D(x, y);
        }

        public void Do()
        {
            var box = new Box(_position);
            
            if (BoxWrapper == null)
            {
                BoxWrapper = new ObjectWrapper<Box>(box);
            }
            else
            {
                BoxWrapper.Object = box;
            }
        }

        public void Undo()
        {
            BoxWrapper?.Object.Dispose();
        }
    }

    class DeleteBoxAction : IAction
    {
        private readonly Transform2D _position;
        private ObjectWrapper<Box> _box;

        public DeleteBoxAction(ObjectWrapper<Box> box)
        {
            _position = box.Object.Transform2D;
            _box = box;
        }

        public void Do()
        {
            _box.Object.Dispose();
        }

        public void Undo()
        {
            _box.Object = new Box(_position);
        }
    }
}
