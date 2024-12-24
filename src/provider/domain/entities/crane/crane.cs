using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane
{
    public class Crane : Entity<CraneId>
    {
        private CraneBrand _brand;
        private CraneModel _model;
        private CranePlate _plate;
        private CraneType _type;
        private CraneYear _year;
        private bool _isActive = true;

        public Crane(CraneId id, CraneBrand brand, CraneModel model, CranePlate plate, CraneType type, CraneYear year) : base(id)
        {
            _brand = brand;
            _model = model;
            _plate = plate;
            _type = type;
            _year = year;
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
    }
}
