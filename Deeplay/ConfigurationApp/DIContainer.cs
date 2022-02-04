using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConfigurationApp
{
    public static class DIContainer
    {
        private static readonly Dictionary<Type, Type> _registeredObjects = new Dictionary<Type, Type>();

        public static dynamic Resolve<TKey>()
        {
            return CreateInstanceWithCtor(typeof(TKey));
        }

        public static dynamic Resolve(Type type)
        {
            return CreateInstanceWithCtor(type);
        }

        public static void Register<TKey, TConcrete>() where TConcrete : TKey
        {
            _registeredObjects[typeof(TKey)] = typeof(TConcrete);
        }

        private static dynamic CreateInstanceWithCtor(Type type)
        {
            var instanceType = _registeredObjects[type];
            var createdClassCtor = instanceType.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();

            if (createdClassCtor.GetParameters().Length == 0 || !type.GetTypeInfo().IsInterface)
                return Activator.CreateInstance(instanceType);

            var ctorArgs = GetServices(createdClassCtor);

            return Activator.CreateInstance(instanceType, args: ctorArgs);
        }

        public static object[] GetServices(ConstructorInfo ctor)
        {
            var ctorParams = ctor.GetParameters();
            object[] services = new object[ctorParams.Length];

            for (int i = 0; i < ctorParams.Length; i++)
            {
                try
                {
                    var service = DIContainer.Resolve(ctorParams[i].ParameterType);
                    services[i] = service;
                }
                catch (Exception ex)
                {
                    throw new Exception("При получении сервиса произошла ошибка: " + ex.Message);
                }
            }

            return services;
        }
    }
}
