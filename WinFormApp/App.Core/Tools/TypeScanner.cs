using App.Core.Init;
using App.Core.UseAge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Tools
{
    public class TypeScanner : InitIoc, ITypeScanner
    {
        public List<Type> GetTypesOf<T>()
        {
            var assemblies = GetLocalAssemblies();
            var manyTypes = assemblies
                .SelectMany(x => x.GetTypes());

            return manyTypes
                .Where(x => typeof(T).IsAssignableFrom(x)
                            && x.IsClass)
                .Where(x => x != typeof(T))
                .ToList();
        }
        public List<Type> GetTypesOfThisAssembly<T>()
        {

            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(x => typeof(T).IsAssignableFrom(x)
                            && x.IsClass)
                .Where(x => x != typeof(T))
                .ToList();
        }
        public List<Type> GetTypesOf<T>(Assembly assembly)
        {
            var manyTypes = assembly.GetTypes();
            return manyTypes
                .Where(x => typeof(T).IsAssignableFrom(x)
                            && x.IsClass)
                .Where(x => x != typeof(T))
                .ToList();
        }

        static IEnumerable<Assembly> GetLocalAssemblies()
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string path = new Uri(Path.GetDirectoryName(callingAssembly.CodeBase)).AbsolutePath;

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic && new Uri(x.CodeBase).AbsolutePath.Contains(path)).ToList();
        }
        public static IEnumerable<Type> GetImplTypes(Type type)
        {
            var types = Assembly.GetAssembly(type).GetTypes()
                .Where(x => x.IsClass && x.GetInterfaces().Contains(type) && x.GetInterfaces().Count() >= 2)
                .ToList();
            return types;
        }
    }
}
