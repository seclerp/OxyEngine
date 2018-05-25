using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OxyEngine
{
  public static class DynamicFactory
  {
    private static readonly Dictionary<Type, object> ObjectActivatorCache = new Dictionary<Type, object>();
    
    private delegate T ObjectActivator<out T>(params object[] args);

    /// <summary>
    ///   Returns dynamically created instance of type T with given args in constructor
    /// </summary>
    /// <param name="args">Arguments to constructor</param>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>New instance of given type</returns>
    public static T NewInstance<T>(params object[] args)
    {
      var type = typeof(T);

      if (!ObjectActivatorCache.ContainsKey(type))
      {
        AddObjectActivator<T>();
      }

      return ((ObjectActivator<T>) ObjectActivatorCache[type]).Invoke(args);
    }

    /// <summary>
    ///   Creates optimized activator for given type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private static void AddObjectActivator<T>()
    {     
      var type = typeof(T);

      var ctor = type.GetConstructors().First();
      var createdActivator = GetActivator<T>(ctor);

      ObjectActivatorCache.Add(type, createdActivator);
    }
    
    private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
    {
      var type = ctor.DeclaringType;
      var paramsInfo = ctor.GetParameters();

      // Create a single param of type object[]
      var param = Expression.Parameter(typeof(object[]), "args");

      var argsExp = new Expression[paramsInfo.Length];

      // Pick each arg from the params array 
      // and create a typed expression of them
      for (var i = 0; i < paramsInfo.Length; i++)
      {
        var index = Expression.Constant(i);
        var paramType = paramsInfo[i].ParameterType;

        var paramAccessorExp = Expression.ArrayIndex(param, index);
        var paramCastExp = Expression.Convert(paramAccessorExp, paramType);

        argsExp[i] = paramCastExp;
      }

      // Make a NewExpression that calls the
      // ctor with the args we just created
      var newExp = Expression.New(ctor, argsExp);

      // Create a lambda with the New
      // Expression as body and our param object[] as arg
      var lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

      // Compile it
      var compiled = (ObjectActivator<T>) lambda.Compile();
      return compiled;
    }
  }
}