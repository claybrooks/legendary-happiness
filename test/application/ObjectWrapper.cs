namespace test.application
{
    public class ObjectWrapper<T>
    {
        public T Object;

        public ObjectWrapper(T @object)
        {
            Object = @object;
        }

    }
}
