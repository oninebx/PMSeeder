using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PMSFake.Core.Extensions
{
    public static class PropertyInfoExtension
    {
    private static readonly MethodInfo? _creatingSetterMethod =
      typeof(PropertyInfoExtension).GetMethod(nameof(CreateGenericSetter), BindingFlags.Static | BindingFlags.NonPublic);
        public static Action<T, object> CreateSetter<T>(this PropertyInfo property) where T : IEntity
        {
            if(property is null)
              {
                  throw new ArgumentNullException(nameof(property));
              }
              var setter = property.GetSetMethod();
              if(setter is null)
              {
                  throw new ArgumentException($"Property({property.Name}) doesn't have a setter");
              }
              if(_creatingSetterMethod == null) {
                throw new NullReferenceException(nameof(_creatingSetterMethod));
              }
              
              var genericCreater = _creatingSetterMethod.MakeGenericMethod(property.DeclaringType!, property.PropertyType);
              return (Action<T, object>)genericCreater.Invoke(null, new object[]{setter})!;
              
        }

    private static Action<T, object> CreateGenericSetter<T, V>(MethodInfo setter) where T : IEntity
    {
      var setterDelegate = (Action<T, V>)Delegate.CreateDelegate(typeof(Action<T, V>), setter);
      return (T instance, object value) => {setterDelegate(instance, (V)value);};
    }
   
  }
}