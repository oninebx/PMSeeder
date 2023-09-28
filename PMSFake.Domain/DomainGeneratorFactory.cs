// /Users/ryanxu/Projects/PMSFake/PMSFake.Domain/DomainGeneratorFactory.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 3/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
using System.Reflection.Emit;
using PMSFake.Core;
using PMSFake.Domain.Entities;
using PMSFake.Domain.Generators;

namespace PMSFake.Domain
{
	public class DomainGeneratorFactory : IDomainGeneratorFactory
	{
        //private IDictionary<string, GeneratorGroup<IEntity>> _cache;
        private IGeneratorFactory<string> _stringFactory;
        private IDomainGenerator<Patient>? _patientGenerator;
		public DomainGeneratorFactory(IGeneratorFactory<string> stringFactory)
		{
            _stringFactory = stringFactory;
            
            //_cache = new Dictionary<string, GeneratorGroup<IEntity>>();
		}

        public IDomainGenerator<Patient> CreatePatientGenerator()
        {
            var nhiGenerator = _stringFactory.Create<NHIGenerator>();
            if(_patientGenerator == null)
            {
                _patientGenerator = new DomainGenerator<Patient>();
                _patientGenerator.ValueFor(p => p.NHI, nhiGenerator.Generate);
            }
            
            
            return _patientGenerator;
            
        }
    }
}

