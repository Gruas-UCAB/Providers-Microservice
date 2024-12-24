namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class CraneNotFoundException : Exception
    {
        public CraneNotFoundException() : base("Crane not found") { }
    }
}
