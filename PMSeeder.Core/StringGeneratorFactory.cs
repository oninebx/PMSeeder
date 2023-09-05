// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/StringGeneratorFactory.cs
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
	public class StringGeneratorFactory : IGeneratorFactory<string>
	{
        private readonly IDictionary<string, IGenerator<string>> _cache;
        private readonly IGeneratorConfiguration _config;
		public StringGeneratorFactory(IGeneratorConfiguration config)
		{
            _cache = new Dictionary<string, IGenerator<string>>();
            _config = config;
		}

        public G Create<G>() where G : IGenerator<string>
        {
            var generatorType = typeof(G);
            IGenerator<string>? generator;
            var key = generatorType.Name;
            if (_cache.TryGetValue(key, out generator))
            {
                return (G)generator;
            }
            else
            {
                generator = (IGenerator<string>?)Activator.CreateInstance(generatorType, new object[] { _config });
                if(generator != null)
                {
                    _cache.Add(key, generator);
                    return (G)generator;
                }
            }

            throw new NotSupportedException($"Generator type({key}) is not supported.");
        }
    }
}

