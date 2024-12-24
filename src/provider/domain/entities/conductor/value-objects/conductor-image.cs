using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects
{
    public class ConductorImage : IValueObject<ConductorImage>
    {
        public string _image;

        public ConductorImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                throw new InvalidConductorImageException();
            }
            _image = image;
        }
        
        public string GetImage()
        {
            return _image;
        }

        public bool Equals(ConductorImage other)
        {
            return _image == other._image;
        }
    }
}
