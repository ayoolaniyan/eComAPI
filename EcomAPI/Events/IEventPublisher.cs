namespace EcomAPI.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent evt);        
    }
}
