using UndoRedo;

namespace test.application.actions
{
    class MoveBoxAction : IUndoRedoAction
    {
        private readonly Transform2D _current;
        private readonly Transform2D _new;
        private readonly ObjectWrapper<Box> _box;

        public MoveBoxAction(ObjectWrapper<Box> box, Transform2D transform)
        {
            _box = box;
            _current = box.Object.Position;
            _new = transform;
        }

        public void Do()
        {
            _box.Object.Position = _new;
        }

        public void Undo()
        {
            _box.Object.Position = _current;
        }
    }

    class CreateBoxAction : IUndoRedoAction
    {
        private readonly Transform2D _position;
        public ObjectWrapper<Box> BoxWrapper;

        public CreateBoxAction(ObjectWrapper<Box> box, int x, int y)
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

    class DeleteBoxAction : IUndoRedoAction
    {
        private readonly Transform2D _position;
        private ObjectWrapper<Box> _box;

        public DeleteBoxAction(ObjectWrapper<Box> box)
        {
            _position = box.Object.Position;
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
