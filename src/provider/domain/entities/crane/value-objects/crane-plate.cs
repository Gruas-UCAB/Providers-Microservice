﻿using ProvidersMicroservice.core.Common;
using ProvidersMicroservice.src.provider.domain.entities.crane.exceptions;
using UsersMicroservice.Core.Domain;

namespace ProvidersMicroservice.src.provider.domain.entities.crane.value_objects
{
    public class CranePlate : IValueObject<CranePlate>
    {
        private string _plate;

        public CranePlate(string plate)
        {
            if (CranePlateValidator.IsValid(plate.ToUpper()))
            {
                _plate = plate.ToUpper();
            }
            else
            {
                throw new InvalidCranePlateException();
            }
        }

        public string GetPlate()
        {
            return _plate;
        }
        public bool Equals(CranePlate other)
        {
            return _plate == other.GetPlate();
        }
    }
}
