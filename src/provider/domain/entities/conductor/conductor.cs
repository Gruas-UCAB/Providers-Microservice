
using ProvidersMicroservice.src.provider.domain.entities.conductor.exceptions;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.conductor
{
    public class Conductor(ConductorId id, ConductorDni dni, ConductorName name, ConductorImage image, CraneId? craneId) : Entity<ConductorId>(id)    
    {
        private ConductorDni _dni = dni;
        private ConductorName _name = name;
        private ConductorImage _image = image;
        private CraneId? _assignedCrane = craneId;
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

        public string GetImage()
        {
            return _image.GetImage();
        }

        public string GetAssignedCrane()
        {
            if (_assignedCrane == null)
            {
                return null;
            }
            return _assignedCrane.GetId();
        }

        public void AssignCrane(CraneId crane)
        {   
            if (_assignedCrane != null)
            {
                UnassignCrane(_assignedCrane);
                //throw new ConductorAlreadyHasCraneAssignedException();
            }
            _assignedCrane = crane;
        }

        public void UnassignCrane(CraneId craneId)
        {
            if (_assignedCrane == null)
            {
                throw new ConductorNotHaveCraneAssignedException();
            }
            if (_assignedCrane.GetId() != craneId.GetId())
            {
                throw new CraneIsNotFromConductorException();
            }
            _assignedCrane = null;
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
