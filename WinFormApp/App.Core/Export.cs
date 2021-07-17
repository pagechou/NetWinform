using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Ioc;
using App.Core.Redis;
using App.Core.UseAge;
using ServiceStack.Redis;

namespace App.Core
{
    public class Export
    {
        public static IocBox IocBox => IocBox.Instance;

        public static ICache Cache => IocBox.Resolve<ICache>();

        public static RedisClient RedisClient => IocBox.Resolve<RedisManage>().Client;

    }
}
