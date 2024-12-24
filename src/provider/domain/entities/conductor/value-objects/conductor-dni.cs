using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects
{
    public class ConductorDni : IValueObject<ConductorDni>
    {
        private int _dni;

        public ConductorDni(int dni)
        {
            if (dni < 4000000)
            {
                throw new InvalidConductorDniException( );
            }
            _dni = dni;
        }

        public int GetDni()
        {
            return _dni;
        }
        public bool Equals(ConductorDni other)
        {
            return _dni == other.GetDni();
        }
    }
}
