
using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor
{
    public class Conductor(ConductorId id, ConductorDni dni, ConductorName name, ConductorLocation location, ConductorImage image, CraneId craneId) : Entity<ConductorId>(id)    
    {
        private ConductorDni _dni = dni;
        private ConductorName _name = name;
        private ConductorLocation _location = location;
        private ConductorImage _image = image;
        private CraneId _assignedCrane = craneId;
        private bool _isActive = true;

        public string GetId()
        {
            return _id.GetId();
        }

        public int GetDni()
        {
            return _dni.GetDni();
        }

        public string GetName()
        {
            return _name.GetName();
        }

        public string GetLocation()
        {
            return _location.GetLocation();
        }

        public string GetImage()
        {
            return _image.GetImage();
        }

        public string GetAssignedCrane()
        {
            return _assignedCrane.GetId();
        }

        public void ChangeLocation(ConductorLocation location)
        {
            _location = location;
        }

            public void ChangeImage(ConductorImage image)
        {
            _image = image;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }

        public bool IsActive()
        {
            return _isActive;
        }
    }
}
