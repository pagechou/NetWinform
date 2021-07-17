using App.Core.Init;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Redis
{
    public class RedisManage : InitIoc
    {
        private object lockObj = new object();
        private RedisManage() { }
        public RedisClient Client
        {
            get
            {
                if (Client == null)
                {
                    lock (lockObj)
                    {
                        if (Client == null)
                        {
                            Client = new RedisClient("one-redis314.redis.rds.aliyuncs.com", 6379, "pagechou:Passw0rd");
                        }
                    }
                }
                return Client;
            }
            private set { }
        }
    }
}
