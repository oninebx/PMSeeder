// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Domain/IDomainGeneratorFactory.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 3/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
using PMSeeder.Core;
using PMSeeder.Domain.Entities;

namespace PMSeeder.Domain
{
	public interface IDomainGeneratorFactory
	{
        PatientGenerator CreatePatientGenerator();
        AppointmentGenerator CreateAppointmentGenerator();
    }
}

