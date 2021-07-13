using Castle.DynamicProxy;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using App.Core.Ioc;
using LinqToDB.Data;
using System.Configuration;
using App.Core.Tools;
using App.Core.Extends;
using App.Core.UseAge;

namespace App.Core.Service
{
    public class AppService : IServiceBase, IDisposable
    {
        public static DataConnection DataBase { get; private set; }

        public string ID { get; }

        public AppService()
        {
            ID = Guid.NewGuid().ToString("N");
        }

        protected virtual DataConnection GetDbContext(string connectionStringName = "default")
        {
            ConnectionStringSettings item = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (item == null || item.ConnectionString.IsNullOrEmpty())
            {
                throw new Exception(string.Concat("请配置数据库连接字符串：", connectionStringName));
            }
            //string strAesDecrypt = SimpleCipherHelper.AesDecrypt(item.ConnectionString);
            if (DB == null)
            {
                DB = new DataConnection(connectionStringName);
            }
            return DB;
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        public virtual void Dispose()
        {

        }
        ~AppService()
        {

        }
    }

    public class AppService<T> : AppService
        where T : class
    {
        private static IWindsorContainer box => IocBox.Instance.Box;
        private static ProxyGenerator generator = new ProxyGenerator();
        public static T Proxy
        {
            get
            {
                string typeStr = string.Concat("appservice.proxy:", typeof(T).FullName.ToLower());
                Type type = box.Resolve<T>().GetType();
                object cacheObj = Export.Cache.Get(typeStr, str => (T)generator.CreateClassProxy(type, IocBox.Instance.DBInterceptor));
                return (T)cacheObj;
            }
        }
    }
}
