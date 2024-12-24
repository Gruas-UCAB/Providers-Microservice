using ProvidersMicroservice.src.provider.application.repositories.dto;
using ProvidersMicroservice.src.provider.domain.entities.conductor;
using ProvidersMicroservice.src.provider.domain.entities.conductor.value_objects;
using ProvidersMicroservice.src.provider.domain.entities.crane;
using ProvidersMicroservice.src.provider.domain.entities.crane.value_objects;
using ProvidersMicroservice.src.provider.domain.events;
using ProvidersMicroservice.src.provider.domain.exceptions;
using ProvidersMicroservice.src.provider.domain.value_objects;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain
{
    public class Provider(ProviderId id) : AggregateRoot<ProviderId>(id)
    {
        private ProviderRif _rif;
        private ProviderName _name;
        private ProviderImage _image;
        private List<Crane> _cranes = [];
        private List<Conductor> _conductors = [];
        private bool _isActive = true;
        protected override void ValidateState()
        {
            if (_rif == null || _name == null || _image == null)
            {
                throw new InvalidProviderException();
            }
        }

        public string GetId()
        {
            return _id.GetId();
        }

        public string GetRif()
        {
            return _rif.GetRif();
        }

        public string GetName()
        {
            return _name.GetName();
        }

        public string GetImage()
        {
            return _image.GetImage();
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void ChangeStatus()
        {
            _isActive = !_isActive;
        }

        public List<Crane> GetCranes()
        {
            return _cranes;
        }

        public List<Conductor> GetConductors()
        {
            return _conductors;
        }

        public void AssignCraneToConductor(CraneId craneId, Conductor conductor)
        {
            Apply(CraneAssignedToConductor.CreateEvent(_id, craneId, new ConductorId(conductor.GetId())));
        }

        public void RemoveCraneFromConductor(Conductor conductor, CraneId craneId)
        {
            Apply(CraneUnassignedToConductor.CreateEvent(_id, craneId, new ConductorId(conductor.GetId())));
        }

        public static Provider Create(ProviderId id, ProviderName name, ProviderRif rif, ProviderImage image, List<Conductor> conductors, List<Crane> cranes)
        {
            Provider provider = new(id);
            provider.Apply(ProviderCreated.CreateEvent(id, rif, name, image, conductors ?? [], cranes ?? []));
            return provider;
        }

        public Crane AddCrane(CraneId id, CraneBrand brand, CraneModel model, CranePlate plate, CraneType type, CraneYear year)
        {
            Apply(CraneCreated.CreateEvent(_id, id, brand, model, plate, type, year));
            return _cranes.Last();
        }
        public Conductor AddConductor(ConductorId id, ConductorDni dni, ConductorName name, ConductorImage image, CraneId? crane)
        {
            Apply(ConductorCreated.CreateEvent(_id, id, dni, name, image, crane ?? null));
            return _conductors.Last();
        }
        private void OnProviderCreatedEvent(ProviderCreated Event)
        {
            _rif = new ProviderRif(Event.Rif);
            _name = new ProviderName(Event.Name);
            _image = new ProviderImage(Event.Image);
            _conductors = Event.Conductors;
            _cranes = Event.Cranes;
        }
        private Conductor OnConductorCreatedEvent(ConductorCreated Event)
        {
            var conductor = new Conductor(
                    new ConductorId(Event.Id),
                    new ConductorDni(Event.Dni),
                    new ConductorName(Event.Name),
                    new ConductorImage(Event.Image),
                    Event.CraneId != null ? new CraneId(Event.CraneId) : null);
            _conductors.Add(conductor);
            return conductor;
        }
        private Crane OnCraneCreatedEvent(CraneCreated Event)
        {
            var crane = new Crane(
                    new CraneId(Event.Id),
                    new CraneBrand(Event.Brand),
                    new CraneModel(Event.Model),
                    new CranePlate(Event.Plate),
                    new CraneType(Event.Type),
                    new CraneYear(Event.Year)
                );
            _cranes.Add(crane);
            return crane;
        }
        private void OnCraneAssignedToConductorEvent(CraneAssignedToConductor Event)
        {
            var conductor = _conductors.Find(conductor => conductor.GetId() == Event.ConductorId);
            conductor.AssignCrane(new CraneId(Event.CraneId));
        }
        private void OnCraneUnassignedToConductorEvent(CraneUnassignedToConductor Event)
        {
            var conductor = _conductors.Find(conductor => conductor.GetId() == Event.ConductorId);
            conductor.UnassignCrane(new CraneId(Event.CraneId));
        }
    }
}
