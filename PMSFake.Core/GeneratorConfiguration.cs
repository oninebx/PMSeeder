// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/GeneratorConfiguration.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 4/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
namespace PMSFake.Core
{
	public class GeneratorConfiguration : IGeneratorConfiguration
	{
		public bool IsNewFormat { get; set; }

		public GeneratorConfiguration()
		{
		}

    }
}

