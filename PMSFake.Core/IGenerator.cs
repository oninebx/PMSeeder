// ${CopyrightHolder}
// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/IGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
using System;
using System.Linq.Expressions;

namespace PMSFake.Core
{
	public interface IGenerator<T>
	{
		T Generate();
	}
}

