namespace Common
{
    public interface IInitializable<T>
    {
        void Init(T data);
    }
}