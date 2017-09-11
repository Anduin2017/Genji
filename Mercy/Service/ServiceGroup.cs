using Mercy.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mercy.Service
{
    public class ServiceGroup
    {
        public List<Type> Services { get; set; } = new List<Type>();

        public ServiceGroup RegisterController<T>() where T : class
        {
            return Register<T>();
        }
        public ServiceGroup Register<T>() where T : class
        {
            Type typeParameterType = typeof(T);
            this.Services.Add(typeParameterType);
            return this;
        }

        public object GetService(Type type)
        {
            if (!Services.Exists(t => t.Equals(type)))
            {
                throw new ServiceNotRegistered();
            }
            var constructor = type.GetConstructors()[0];
            var args = constructor.GetParameters();
            object[] parameters = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var requirement = args[i].ParameterType;
                parameters[i] = GetService(requirement);
            }
            var instance = Assembly.GetAssembly(type).CreateInstance(type.FullName, true, BindingFlags.Default, null, parameters, null, null);
            return instance;
        }
    }
}
