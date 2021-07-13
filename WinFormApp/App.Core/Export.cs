using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Ioc;
using App.Core.UseAge;

namespace App.Core
{
    public class Export
    {
        public static IocBox IocBox => IocBox.Instance;

        public static ICache Cache => IocBox.Resolve<ICache>();
    }
}
