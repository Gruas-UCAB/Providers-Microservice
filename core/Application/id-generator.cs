namespace UsersMicroservice.core.Application
{
    public interface IIdGenerator<T>
    {
        T GenerateId();
    }
}
