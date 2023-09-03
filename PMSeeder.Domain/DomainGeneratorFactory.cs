// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Domain/DomainGeneratorFactory.cs
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
using PMSeeder.Core;
using PMSeeder.Domain.Entities;

namespace PMSeeder.Domain
{
	public class DomainGeneratorFactory : IDomainGeneratorFactory
	{
        //private IDictionary<string, GeneratorGroup<IEntity>> _cache;
        private IGeneratorFactory<string> _stringFactory;
        private PatientGenerator? _patientGenerator;
		public DomainGeneratorFactory(IGeneratorFactory<string> stringFactory)
		{
            _stringFactory = stringFactory;
            
            //_cache = new Dictionary<string, GeneratorGroup<IEntity>>();
		}

        //private G Create<G, E>() where G : GeneratorGroup<E> where E : IEntity
        //{
        //    var generatorType = typeof(G);
        //    GeneratorGroup<IEntity>? generator;
        //    string key = generatorType.Name;
        //    if(_cache.TryGetValue(key, out generator))
        //    {
        //        return (G)generator;
        //    }else
        //    {
        //        generator = (GeneratorGroup<IEntity>?)Activator.CreateInstance(generatorType);
        //        if(generator != null)
        //        {
        //            _cache.Add(key, generator);
        //            return (G)generator;
        //        }
                
        //    }
        //    throw new NotSupportedException($"Generator type({key}) is not supported.");
        //}

        public AppointmentGenerator CreateAppointmentGenerator()
        {
            throw new NotImplementedException();
        }

        public PatientGenerator CreatePatientGenerator()
        {
            var nhiGenerator = _stringFactory.Create<NHIGenerator>();
            if(_patientGenerator == null)
            {
                _patientGenerator = new PatientGenerator();
                _patientGenerator.AddGenerator<string>("NHI", nhiGenerator);
            }
            
            
            return _patientGenerator;
            
        }
    }
}

