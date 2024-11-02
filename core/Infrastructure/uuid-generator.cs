using UsersMicroservice.core.Application;

namespace UsersMicroservice.core.Infrastructure
{
    public class UUIDGenerator : IIdGenerator<string>
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
