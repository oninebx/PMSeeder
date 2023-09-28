// /Users/ryanxu/Projects/PMSFake/PMSFake.Domain/IDomainGeneratorFactory.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 3/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
using PMSFake.Core;
using PMSFake.Domain.Entities;

namespace PMSFake.Domain
{
	public interface IDomainGeneratorFactory
	{
        IDomainGenerator<Patient> CreatePatientGenerator();
    }
}

