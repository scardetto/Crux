using System;
using System.Reflection;
using Crux.Core.Extensions;

namespace Crux.Core
{
    public static class ClassLoader
    {
        public static object New(string typeName)
        {
            if (!typeName.Contains(",")) {
                typeName = "{0}, {1}".ToFormat(typeName, Assembly.GetCallingAssembly().FullName);
            }

            return New(typeName, typeof(object));
        }

        public static T New<T>(string assemblyPath, string typeName)
        {
            return New<T>(Assembly.LoadFrom(assemblyPath), typeName);
        }

        public static T New<T>(Type clazz)
        {
            return (T)New(clazz, typeof(T));
        }

        public static T New<T>(string typeName)
        {
            if (!typeName.Contains(",")) {
                typeName = "{0}, {1}".ToFormat(typeName, Assembly.GetCallingAssembly().FullName);
            }

            return (T)New(typeName, typeof(T));
        }

        public static object New(string typeName, Type desiredType)
        {
            return New(GetTypeFromName(typeName, Type.GetType), desiredType);
        }

        public static object New(Type clazz, Type desiredType)
        {
            if (desiredType != null && !desiredType.IsAssignableFrom(clazz)) {
                throw new ActivationException(String.Format("The type {0} cannot be assigned to type {1}", clazz.Name, desiredType.Name));
            }

            return New(clazz);
        }

        public static object New(string assemblyPath, string typeName, Type desiredType)
        {
            return New(Assembly.LoadFrom(assemblyPath), typeName, desiredType);
        }

        public static T New<T>(Assembly assembly, string typeName)
        {
            return (T)New(assembly, typeName, typeof(T));
        }

        public static object New(Assembly assembly, string typeName, Type desiredType)
        {
            return New(GetTypeFromName(typeName, assembly.GetType), desiredType);
        }

        public static object New(Type clazz)
        {
            return Activator.CreateInstance(clazz);
        }

        private static Type GetTypeFromName(string typeName, Func<string, Type> accessor)
        {
            var type = accessor(typeName);

            if (type == null) {
                throw new ActivationException(String.Format("Could not find type {0}", typeName));
            }

            return type;
        }
    }

    public class ActivationException : ApplicationException
    {
        public ActivationException() { }

        public ActivationException(string s) : base(s) { }

        public ActivationException(string s, Exception e) : base(s, e) { }
    }
}
