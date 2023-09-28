// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/DataLoader.cs
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
	public class DataLoader : IDataLoader
	{
        public IList<string> FromFile(string fileName)
        {
            IList<string> data = new List<string>();
            if(string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException($"{fileName} cannot be null");
            }

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"{fileName} doesn't exist");
            }
            data = File.ReadAllLines(fileName);
            
            return data;
        }
    }
}

