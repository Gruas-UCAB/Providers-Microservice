namespace UsersMicroservice.Core.Domain
{
    public class DomainEvent<T>(string dispatcherId, string name, T context) 
    {
        public string DispatcherId = dispatcherId;
        public string Name = name;
        public DateTime Timestamp = DateTime.UtcNow;
        public T Context = context;
    }
}
