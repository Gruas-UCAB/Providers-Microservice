namespace UsersMicroservice.Core.Domain
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
        public override string ToString()
        {
            return GetType().Name + ": " + Message;
        }

        public static string ExceptionName
        {
            get
            {
                return typeof(DomainException).Name;
            }
        }
    }
}
