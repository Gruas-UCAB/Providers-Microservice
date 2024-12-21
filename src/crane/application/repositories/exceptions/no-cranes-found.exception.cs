namespace ProvidersMicroservice.src.crane.application.repositories.exceptions
{
    public class NoCranesFoundException : Exception
    {
        public NoCranesFoundException() : base("No cranes found") { } 
    }
}
