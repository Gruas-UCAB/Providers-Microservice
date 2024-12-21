using ProvidersMicroservice.src.crane.domain.events;
using ProvidersMicroservice.src.crane.domain.exceptions;
using ProvidersMicroservice.src.crane.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.crane.domain
{
    public class Crane(CraneId id) : AggregateRoot<CraneId>(id)
    {
        private CraneBrand _brand;
        private CraneModel _model;
        private CranePlate _plate;
        private CraneType _type;
        private CraneYear _year;
        private bool _isActive = true;

        protected override void ValidateState()
        {
            if (_brand == null || _model == null || _plate == null || _type == null || _year == null)
            {
                throw new InvalidCraneException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }

        public string GetBrand()
        {
            return _brand.GetBrand();
        }

        public string GetModel()
        {
            return _model.GetModel();
        }

        public string GetPlate()
        {
            return _plate.GetPlate();
        }

        public string GetType()
        {
            return _type.GetType();
        }

        public int GetYear()
        {
            return _year.GetYear();
        }

        public bool IsActive() 
        {
            return _isActive;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }

        public static Crane Create(CraneId id, CraneBrand brand, CraneModel model, CranePlate plate, CraneType type, CraneYear year)
        {
            Crane crane = new (id);
            crane.Apply(CraneCreated.CreateEvent(id, brand, model, plate, type, year));
            return crane;
        }

        private void OnCraneCreatedEvent(CraneCreated Event)
        {
            _brand = new CraneBrand(Event.Brand);
            _model = new CraneModel(Event.Model);
            _plate = new CranePlate(Event.Plate);
            _type = new CraneType(Event.Type);
            _year = new CraneYear(Event.Year);
        }
    }
}
