namespace committed
{
    public interface IAction
    {
        void Do();
        void Undo();
    }

}
