namespace Messenger
{
    public interface IMessageListener<T>
    {
        void OnMessage(T message);
    }
}