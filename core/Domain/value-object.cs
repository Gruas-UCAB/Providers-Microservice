namespace UsersMicroservice.Core.Domain
{
    public interface IValueObject<T>
    {
        bool Equals(T other);
    }
}
