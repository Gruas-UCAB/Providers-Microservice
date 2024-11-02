using System.Reflection;

namespace UsersMicroservice.Core.Domain
{
    public abstract class AggregateRoot<T>(T id) : Entity<T>(id) where T : IValueObject<T>
    {
        private List<DomainEvent<object>> _events = new List<DomainEvent<object>>();

        protected abstract void ValidateState();

        public List<DomainEvent<object>> PullEvents()
        {
            List<DomainEvent<object>> events = _events;
            _events = [];
            return events;
        }

        public void Apply(DomainEvent<object> Event, bool fromHistory = false)
        {
            MethodInfo? Handler = GetEventHandler(Event) ?? throw new Exception($"Handler not found for event: {Event.GetType().Name}");
            if (!fromHistory) _events.Add(Event);
            Handler.Invoke(this, new object[] { Event.Context });
            ValidateState();
        }

        private protected MethodInfo? GetEventHandler(DomainEvent<object> Event)
        {
            MethodInfo? Handler = GetType().GetMethod(
                $"On{Event.GetType().Name}", 
                BindingFlags.Instance | 
                BindingFlags.NonPublic | 
                BindingFlags.Public);
            return Handler;
        }

        private void Hydrate(List<DomainEvent<object>> history)
        {
            if (history.Count == 0) throw new Exception("No events to replay");
            history.ForEach(Event => { Apply(Event, true); });
        }
    }
}
