// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/IGeneratorFactory.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
namespace PMSeeder.Core
{
	public interface IGeneratorFactory<T>
	{
        public G Create<G>() where G : IGenerator<T>;

    }
}

