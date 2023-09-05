// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/GeneratorConfiguration.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 4/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
namespace PMSeeder.Core
{
	public class GeneratorConfiguration : IGeneratorConfiguration
	{
		public bool SupportNewNHIFormat { get; set; }

		public GeneratorConfiguration()
		{
		}

    }
}

