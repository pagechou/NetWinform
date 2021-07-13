using App.Core.Init;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.UseAge
{
    public interface ITypeScanner : IInitIoc
    {
        List<Type> GetTypesOf<T>();
        List<Type> GetTypesOf<T>(Assembly assembly);
        List<Type> GetTypesOfThisAssembly<T>();

    }
}
