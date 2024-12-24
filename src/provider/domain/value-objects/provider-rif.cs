using ProvidersMicroservice.core.Common;
using ProvidersMicroservice.src.provider.domain.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.value_objects
{
    public class ProviderRif : IValueObject<ProviderRif>
    {

        private readonly string _rif;

        public ProviderRif(string rif)
        {
            if (!RifValidator.IsValid(rif))
            {
                throw new InvalidProviderRifException();
            }
            _rif = rif.ToUpper();
        }
        public string GetRif()
        {
            return _rif;
        }
        public bool Equals(ProviderRif other)
        {
            throw new NotImplementedException();
        }
    }
}
