namespace UsersMicroservice.core.Common
{
    public class _Optional<T>
    {
        private T? Value;
        
        private _Optional(T? value)
        {
            Value = value;            
        }

        public T Unwrap()
        {
            if (Value == null) throw new Exception("Value is not present");
            return Value;
        }

        public bool HasValue()
        {
            return Value != null;
        }

        public static _Optional<T> Of(T value)
        {
            return new _Optional<T>(value);
        }

        public static _Optional<T> Empty()
        {
            return new _Optional<T>(default);
        }
    }
}
