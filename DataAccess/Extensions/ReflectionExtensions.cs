using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class ReflectiveEnumerator
    {
        static ReflectiveEnumerator() { }

        public static ICollection<string> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<string> objects = new List<string>();
            var types = Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));
            foreach (Type type in types)
            {
                objects.Add(type.Name);
            }
            objects.Sort();
            return objects;
        }
    }

    public static class ReflectionExtensions
    {
        static ReflectionExtensions() { }

        public static Type GetDbConnectorType(this Type type)
        {
            var dbConnectorTypeField = type
            .GetField("DbConnectorType", BindingFlags.Static | BindingFlags.NonPublic);
            var value = dbConnectorTypeField.GetValue(null);

            if (value != null)
                return (Type)value;
            else
                throw new Exception($"DbContext type <{type}> has no DbConnectorType field.");
        }
    }
}
