namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class NoProvidersFoundException : Exception
    {
        public NoProvidersFoundException() : base("No providers found") { }
    }
}
