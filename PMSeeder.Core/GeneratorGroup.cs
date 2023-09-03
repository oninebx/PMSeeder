// /Users/ryanxu/Projects/PMSeeder/PMSeeder.Core/GeneratorGroup.cs
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
	public abstract class GeneratorGroup<E> : IGenerator<E> where E : IEntity
	{
        protected IDictionary<string, IGenerator<string>> _stringGenerators;
		protected GeneratorGroup()
		{
            _stringGenerators = new Dictionary<string, IGenerator<string>>();
		}

        public void AddGenerator<T>(string key, IGenerator<T> generator)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Argument key cannot be null or empty");
            }
            if(generator == null)
            {
                throw new ArgumentNullException("Argument generator cannot be null");
            }
            
            if(typeof(T) == typeof(string))
            {
                _stringGenerators.Add(key, (IGenerator<string>)generator);
            }
        }

        public abstract E Generate();

        public static explicit operator GeneratorGroup<E>(GeneratorGroup<IEntity> v)
        {
            throw new NotImplementedException();
        }
    }
}

