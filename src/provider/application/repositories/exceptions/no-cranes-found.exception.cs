namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class NoCranesFoundException : Exception
    {
        public NoCranesFoundException() : base("No cranes found") { }
    }
}
