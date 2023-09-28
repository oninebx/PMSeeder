// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/IDataLoader.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 11/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
namespace PMSFake.Core
{
	public interface IDataLoader
	{
		IList<string> FromFile(string fileName);
	}
}

