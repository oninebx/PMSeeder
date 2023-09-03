// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Domain/PatientGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.

using System;
using PMSeeder.Core;
using PMSeeder.Domain.Entities;

namespace PMSeeder.Domain
{
	public class PatientGenerator : GeneratorGroup<Patient>
	{
        public PatientGenerator() : base()
        {
            
        }

        public override Patient Generate()
        {
            var patient = new Patient();
            patient.NHI = _stringGenerators["NHI"].Generate();
            return patient;
        }
    }
}

