namespace UsersMicroservice.Core.Domain
{
    public abstract class Entity<T> where T : IValueObject<T>
    {
        protected readonly T _id;

        protected Entity(T id)
        {
            _id = id;
        }

        public T Id => _id;

        public bool Equals(Entity<T> other)
        {
            if (other == null) return false;
            return _id.Equals(other.Id);
        }
    }
}
