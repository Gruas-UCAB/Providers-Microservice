namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class ConductorNotFoundException : Exception
    {
        public ConductorNotFoundException() : base("Conductor not found") { }
    }
}
