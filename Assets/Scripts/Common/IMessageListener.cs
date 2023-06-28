namespace Common
{
    public interface IMessageListener<T>
    {
        void OnMessage(T message);
    }
}