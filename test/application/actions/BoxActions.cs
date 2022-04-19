using committed;

namespace test.application.actions
{
    class MoveBoxAction : IAction
    {
        private readonly Transform2D _current;
        private readonly Transform2D _new;
        private readonly ObjectWrapper<Box> _box;

        public MoveBoxAction(ObjectWrapper<Box> box, Transform2D transform)
        {
            _box = box;
            _current = box.Object.Transform2D;
            _new = transform;
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
        public ObjectWrapper<Box> BoxWrapper;

        public CreateBoxAction(ObjectWrapper<Box> box, float x, float y)
        {
            BoxWrapper = box;
            _position = new Transform2D(x, y);
        }

        public void Do()
        {
            if (BoxWrapper.Object.Deleted)
            {
                var box = new Box(_position);
                BoxWrapper.Object = box;
            }
        }

        public void Undo()
        {
            BoxWrapper.Object.Dispose();
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
