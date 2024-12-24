using UsersMicroservice.Core.Domain;
using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CraneBrand : IValueObject<CraneBrand>
    {
        private readonly string _brand;

        public CraneBrand(string brand)
        {
            if (brand.Length < 2 || brand.Length > 20)
            {
                throw new InvalidCraneBrandException();
            }
            _brand = brand;
        }

        public string GetBrand()
        {
            return _brand;
        }   

        public bool Equals(CraneBrand other)
        {
            throw new NotImplementedException();
        }
    }
}
