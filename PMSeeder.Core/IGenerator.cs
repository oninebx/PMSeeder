// ${CopyrightHolder}
// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/IGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 2/09/2023
using System;
namespace PMSeeder.Core
{
	public interface IGenerator<T>
	{
		T Generate();
	}
}

