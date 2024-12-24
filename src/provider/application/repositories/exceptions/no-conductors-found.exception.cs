namespace ProvidersMicroservice.src.provider.application.repositories.exceptions
{
    public class NoConductorsFoundException : Exception
    {
        public NoConductorsFoundException() : base("No conductors found") { }
    }
}
