// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/PickoutGenerator.cs
// Author: 	ryanxu
// Email:	hitxcl@gmail.com
// Date Created: 9/09/2023
//
// Copyright ( c ) Ryan Xu. All rights reserved.
// Licensed under MIT license.
//
//
using System;
namespace PMSFake.Core
{
	public abstract class PickoutGenerator : IGenerator<string>
	{
        protected IList<string> _data;
        protected Random _random;

		public PickoutGenerator(IList<string> data)
		{
            _data = data;
            _random = new Random();
		}

        public string Generate()
        {
            var index = _random.Next(_data.Count - 1);
            return _data[index];
        }
    }
}

