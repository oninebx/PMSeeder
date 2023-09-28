// /Users/ryanxu/Projects/PMSFake/PMSFake.Domain/Entities/Patient.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
using PMSFake.Core;

namespace PMSFake.Domain.Entities
{
	public class Patient : IEntity
	{
		public string NHI { get; set; } = string.Empty;
		public Patient()
		{

		}
	}
}

