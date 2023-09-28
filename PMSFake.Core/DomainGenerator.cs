// /Users/ryanxu/Projects/PMSFake/PMSFake.Core/DomainGenerator.cs
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
using System.Reflection;
using PMSFake.Core.Extensions;

namespace PMSFake.Core
{
	public class DomainGenerator<E> : IDomainGenerator<E> where E : IEntity
	{
        private readonly IDictionary<string, Func<object>> _populateActions
            = new Dictionary<string, Func<object>>(StringComparer.OrdinalIgnoreCase);
        private readonly IDictionary<string, Action<E, object>> _setterCache = new Dictionary<string, Action<E, object>>(StringComparer.OrdinalIgnoreCase);
        private readonly IDictionary<string, PropertyInfo> _properties;

		public DomainGenerator()
		{
      _properties = typeof(E).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => p.CanWrite)
                            .GroupBy(p => p.Name)
                            .ToDictionary(k => k.Key, g => g.First());
		}

    public E Generate()
    {
        var type = typeof(E);
        var instance = Activator.CreateInstance(type);
        foreach(var action in _populateActions)
        {
          var valueAction = action.Value;
          if(!_setterCache.TryGetValue(action.Key, out var setter))
          {
            if(_properties.TryGetValue(action.Key, out var prop)) 
              {
                setter = prop.CreateSetter<E>();
                _setterCache.Add(prop.Name, setter);
              }
          }
          if(setter is not null && instance is not null && valueAction is not null)
          {
            setter((E)instance, valueAction());
          }
        }
        
        return (E)instance!;
    }

    public IDomainGenerator<E> ValueFor<P>(
            Expression<Func<E, P>> property, Func<P> action)
    {
        var propExpression = property.Body;
        var expString = propExpression.ToString();
        if(expString.IndexOf('.') != expString.LastIndexOf('.'))
        {
            throw new ArgumentException("Nested property is not allowed");
        }
        MemberExpression? member;
        if(propExpression is UnaryExpression unary)
        {
            member = (MemberExpression)unary.Operand;
        }else
        {
            member = propExpression as MemberExpression;
        }
        if(member == null)
        {
            throw new ArgumentException("Expression is not valid");
        }
        var propertyName = member.Member.Name;
        
        _populateActions.Add(propertyName, () => action()!);
        return this;
    }
  }
}

