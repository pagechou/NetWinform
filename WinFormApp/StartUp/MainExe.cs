using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StartUp
{
    static class MainExe
    {
        [STAThread]
        static void Main()
        {
            Assembly assembly = Assembly.Load("App.Winforms");
            var type = assembly.GetType("App.Winforms.StartWinfrom");
            MethodInfo method = type.GetMethod("StartUp", BindingFlags.Static | BindingFlags.Public);
            method.Invoke(type, null);
        }
    }
}
