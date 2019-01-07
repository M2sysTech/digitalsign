namespace Veros.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Reflection
    {
        public static List<T> GetInstancesOfImplementingTypes<T>()
        {
            return GetInstancesOfImplementingTypes<T>(typeof(T).Assembly);
        }

        public static List<T> GetInstancesOfImplementingTypes<T>(Assembly assembly)
        {
            var targetType = typeof(T);
            var types = assembly.GetTypes();

            var list = new List<T>();

            foreach (Type t in types)
            {
                if (!t.IsInterface && !t.IsAbstract)
                {
                    if (t.GetInterfaces().Any(iface => iface == targetType))
                    {
                        list.Add((T)Activator.CreateInstance(t));
                    }
                }
            }

            return list;
        }
    }
}