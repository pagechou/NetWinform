using Castle.DynamicProxy;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using App.Core.Ioc;

namespace App.Core
{
    public class AppService<T> where T:IBase
    {
        private static IWindsorContainer box => IocBox.Instance.Box;
        public static ProxyGenerator generator = new ProxyGenerator();
        public static T Proxy
        {
            get
            {
                Type type = box.Resolve<T>().GetType();
                return (T)generator.CreateClassProxy(type, IocBox.Instance.Interceptor);
            }
        }
    }
}
