using DataAccess.EntityFramework.Contexts;
using System.Reflection;

namespace DataAccess.Extensions
{
    public static class ReflectiveEnumerator
    {
        static ReflectiveEnumerator() { }

        public static ICollection<string> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<string> objects = new List<string>();
            var types = Assembly.GetAssembly(typeof(T))!.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));
            foreach (Type type in types)
            {
                objects.Add(type.Name);
            }
            objects.Sort();
            return objects;
        }
    }
}
