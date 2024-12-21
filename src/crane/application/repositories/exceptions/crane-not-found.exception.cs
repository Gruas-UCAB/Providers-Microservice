namespace ProvidersMicroservice.src.crane.application.repositories.exceptions
{
    public class CraneNotFoundException : Exception
    {
        public CraneNotFoundException() : base("Crane not found") { }
    }
}
