using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhou.Core.Ioc
{
    public sealed class IocBox
    {
        //私有构造
        private IocBox() { }
        private static object m_mutex = new object();
        private static bool m_initialized = false;
        private static int count = 0;
        private static IocBox _box = null;
        private static string _appAss = "";//容器程序集
        public IInterceptor Interceptor => new CommonInterceptor();
        public IWindsorContainer Box { get; private set; }
        // Singleton in thread-safe-mode
        public static IocBox GeInstance()
        {
            if (!m_initialized)
            {
                lock (m_mutex)
                {

                    if (_box == null)
                    {

                        _box = new IocBox();
                        m_initialized = true;
                    }
                }
            }
            return _box;
        }
        /// <summary>
        /// 注册容器
        /// </summary>
        /// <typeparam name="T">基类类型</typeparam>
        /// <param name="box">IWindsorContainer容器</param>
        public void Init<T>(IWindsorContainer box)
        {
            if (count == 0)
            {
                lock (m_mutex)
                {
                    _appAss = AppSetting.Helper.GetAppSettingValue("Zhou.Service", "IocAssemblyName");
                    this.Box = box;
                    box.Register(
                    Classes.FromAssemblyNamed(_appAss)   //选择Assembly
                            .IncludeNonPublicTypes()                    //约束Type
                            .BasedOn<T>()      //约束Type
                            .WithService.DefaultInterfaces()            //匹配类型
                            .LifestyleSingleton()//单例
                    );
                    count++;
                }
            }
        }

    }
}
