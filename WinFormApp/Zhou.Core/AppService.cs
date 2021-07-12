using Castle.DynamicProxy;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhou.Core;
using Zhou.Core.Ioc;

namespace Zhou.Core
{
    public class AppService<T> where T:IBase
    {
        private static IWindsorContainer box => IocBox.GeInstance().Box;
        public static ProxyGenerator generator = new ProxyGenerator();
        public static T Proxy
        {
            get
            {
                Type type = box.Resolve<T>().GetType();
                return (T)generator.CreateClassProxy(type, IocBox.GeInstance().Interceptor);
            }
        }
    }
}
