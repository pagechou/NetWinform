using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Ioc
{
    /// <summary>
    /// IOC管理类(IWindsorContainer容器)
    /// </summary
    public sealed class IocBox
    {
        //私有构造
        private IocBox() { this.Box = new WindsorContainer(); }
        private static object objLock = new object();
        private static bool isLock = false;
        private static int count = 0;
        private static IocBox _box = null;
        private static string _appAssembly = "";//容器程序集
        public IInterceptor Interceptor => new DataBaseInterceptor();
        /// <summary>
        /// 依赖注入容器-属性
        /// </summary>
        public IWindsorContainer Box { get; private set; }
        /// <summary>
        /// 线程安全单例
        /// </summary>
        /// <returns></returns>
        public static IocBox Instance
        {
            get
            {
                if (!isLock)
                {
                    lock (objLock)
                    {

                        if (_box == null)
                        {

                            _box = new IocBox();
                            isLock = true;
                        }
                    }
                }
                return _box;
            }
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
                lock (objLock)
                {
                    _appAssembly = AppSetting.Helper.GetAppSettingValue("App.Service", "IocAssemblyName");
                    this.Box = box;
                    box.Register(
                    Classes.FromAssemblyNamed(_appAssembly)   //选择Assembly
                            .IncludeNonPublicTypes()                    //约束Type
                            .BasedOn<T>()      //约束Type
                            .WithService.DefaultInterfaces()            //匹配类型
                            .LifestyleSingleton()//单例
                    );
                    count++;
                }
            }
        }
        /// <summary>
        /// 判断类型是否已注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>bool</returns>
		public bool IsRegistered<T>()
        {
            return this.IsRegistered(typeof(T));
        }

        /// <summary>
        /// 检测类型是否已注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>bool</returns>
		public bool IsRegistered(Type type)
        {
            return this.Box.Kernel.HasComponent(type);
        }


    }
}
