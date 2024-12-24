using UsersMicroservice.Core.Domain;
using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;
namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CraneModel : IValueObject<CraneModel>
    {
        private readonly string _model;

        public CraneModel(string model) 
        {
            if (model.Length < 2 || model.Length > 20)
            {
                throw new InvalidCraneModelException();
            }
            _model = model;
        }

        public string GetModel()
        {
            return _model;
        }
        public bool Equals(CraneModel other)
        {
            return _model == other.GetModel();
        }
    }
}
