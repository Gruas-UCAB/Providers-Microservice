using ProvidersMicroservice.src.provider.domain.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.value_objects
{
    public class ProviderImage : IValueObject<ProviderImage>
    {
        private readonly string _image;

        public ProviderImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
            {
                throw new InvalidProviderImageException();
            }
            _image = image;
        }
        public string GetImage()
        {
            return _image;
        }
        public bool Equals(ProviderImage other)
        {
            throw new NotImplementedException();
        }
    }
}
