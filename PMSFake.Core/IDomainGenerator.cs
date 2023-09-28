// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/IDomainGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 11/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
using System.Linq.Expressions;

namespace PMSFake.Core
{
	public interface IDomainGenerator<E> : IGenerator<E> where E : IEntity
	{
		IDomainGenerator<E> ValueFor<P>(Expression<Func<E, P>> property, Func<P> action);
	}
}

