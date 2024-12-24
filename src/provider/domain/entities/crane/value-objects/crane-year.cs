using UsersMicroservice.Core.Domain;
using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CraneYear : IValueObject<CraneYear>
    {
        private readonly int _year;

        public CraneYear(int year)
        {
            if (year > 2000 && year < 2024)
            {
                _year = year;
            }
            else
            {
                throw new InvalidCraneYearException();
            }
            
        }

        public int GetYear()
        {
            return _year;
        }

        public bool Equals(CraneYear other)
        {
            throw new NotImplementedException();
        }
    }
}
