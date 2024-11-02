namespace UsersMicroservice.Core.Application
{
    public class ApplicationException : Exception
    {
        protected ApplicationException(string message) : base(message)
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
                return typeof(ApplicationException).Name;
            }
        }
    }
}
