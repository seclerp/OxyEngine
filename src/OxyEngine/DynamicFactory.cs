using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Instrumentation;
using System.Reflection;

namespace OxyEngine
{
  public static class DynamicFactory
  {
    private static Dictionary<Type, object> _objectActivatorCache = new Dictionary<Type, object>();
    private delegate T ObjectActivator<T>(params object[] args);

    /// <summary>
    ///   Returns dynamically created instance of type T with given args in constructor
    /// </summary>
    /// <param name="args">Arguments to constructor</param>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>New instance of given type</returns>
    public static T NewInstance<T>(params object[] args)
    {
      var type = typeof(T);

      if (!_objectActivatorCache.ContainsKey(type))
      {
        AddObjectActivator<T>();
      }

      return ((ObjectActivator<T>) _objectActivatorCache[type]).Invoke(args);
    }

    /// <summary>
    ///   Creates optimized activator for given type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private static void AddObjectActivator<T>()
    {     
      var type = typeof(T);

      ConstructorInfo ctor = type.GetConstructors().First();
      ObjectActivator<T> createdActivator = GetActivator<T>(ctor);

      _objectActivatorCache.Add(type, createdActivator);
    }
    
    private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
    {
      Type type = ctor.DeclaringType;
      ParameterInfo[] paramsInfo = ctor.GetParameters();

      // Create a single param of type object[]
      ParameterExpression param =
        Expression.Parameter(typeof(object[]), "args");

      Expression[] argsExp =
        new Expression[paramsInfo.Length];

      // Pick each arg from the params array 
      // and create a typed expression of them
      for (var i = 0; i < paramsInfo.Length; i++)
      {
        Expression index = Expression.Constant(i);
        Type paramType = paramsInfo[i].ParameterType;

        Expression paramAccessorExp = Expression.ArrayIndex(param, index);
        Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

        argsExp[i] = paramCastExp;
      }

      // Make a NewExpression that calls the
      // ctor with the args we just created
      NewExpression newExp = Expression.New(ctor, argsExp);

      // Create a lambda with the New
      // Expression as body and our param object[] as arg
      LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

      // Compile it
      var compiled = (ObjectActivator<T>) lambda.Compile();
      return compiled;
    }
  }
}