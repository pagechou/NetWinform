using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhou.Core;

namespace Zhou.Core
{
    public class AppService<T>
    {
        private static IWindsorContainer box => IocBox.GeInstance().Box;
        public static T Proxy
        {
            get { return box.Resolve<T>(); }
        }
    }
}
