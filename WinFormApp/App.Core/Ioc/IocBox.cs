using App.Core.Extends;
using App.Core.Init;
using App.Core.Service;
using App.Core.Tools;
using App.Core.UseAge;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
        public IInterceptor DBInterceptor => new DataBaseInterceptor();
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
        /// 应用生存方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="registration">注册组件</param>
        /// <param name="lifeStyle">生存方式枚举值</param>
        /// <returns>ComponentRegistration<T></returns>
		private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, InstanceLifeStyle lifeStyle)
        where T : class
        {
            ComponentRegistration<T> componentRegistration;
            if (registration.Implementation.HasParentType(typeof(IServiceBase)))
            {
                registration = registration.Interceptors<DataBaseInterceptor>();
            }
            if (lifeStyle == InstanceLifeStyle.Singleton)
            {
                componentRegistration = registration.LifestyleSingleton();
            }
            else
            {
                componentRegistration = (lifeStyle == InstanceLifeStyle.Transient ? registration.LifestyleTransient() : registration);
            }
            return componentRegistration;
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
        /// <summary>
        /// 获取依赖对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>T</returns>
        public T Resolve<T>() where T : class
        {
            var t = default(T);
            try
            {
                t = this.Box.Resolve<T>();
            }
            catch
            {
                //if (typeof(T) == typeof(IRepository))
                //{
                //    IocManager.Instance.Register<IRepository, Linq2dbContextRepository>(InstanceLifeStyle.Transient);
                //    t = this.Box.Resolve<T>();
                //}
            }
            return t;
        }

        /// <summary>
        /// 注册指定程序集中的接口
        /// </summary>
        /// <param name="assembly"></param>
		public void RegisterAssemblyByConvention(Assembly assembly)
        {
            BasicRegistrar.RegisterAssembly(assembly);
        }

        /// <summary>
		/// 注册一个类型自身实例
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="lifeStyle">存在形式</param>
		public void Register(Type type, InstanceLifeStyle lifeStyle = 0)
        {
            this.Box.Register(new IRegistration[] { ApplyLifestyle<object>(Component.For(type).OverridesExistingRegistration<object>(), lifeStyle) });
        }

        public void Register<TType>(InstanceLifeStyle lifeStyle = 0)
        where TType : class
        {
            this.Box.Register(new IRegistration[] { ApplyLifestyle<TType>(Component.For<TType>().OverridesExistingRegistration<TType>(), lifeStyle) });
        }

        /// <summary>
        /// 注册多实例Ttype类
        /// </summary>
        /// <typeparam name="TType">类型</typeparam>
        /// <typeparam name="TImpl">接口</typeparam>
		public void RegisterMultiple<TType, TImpl>()
        where TType : class
        where TImpl : class, TType
        {
            ComponentRegistration<TType> componentRegistration = Component.For<TType>().ImplementedBy<TImpl>();
            this.Box.Register(new IRegistration[] { ApplyLifestyle<TType>(componentRegistration, InstanceLifeStyle.Transient) });
        }

        /// <summary>
        /// 带实例注册
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="instance">实例</param>
        /// <param name="lifeStyle">存在形式</param>
		public void RegisterWithIntance(Type type, object instance, InstanceLifeStyle lifeStyle = 0)
        {
            ComponentRegistration<object> componentRegistration = Component.For(type).Instance(instance);
            this.Box.Register(new IRegistration[] { ApplyLifestyle<object>(componentRegistration.OverridesExistingRegistration<object>(), lifeStyle) });
        }


        /// <summary>
        /// 注册容器
        /// </summary>
        /// <typeparam name="T">基类类型</typeparam>
        /// <param name="box">IWindsorContainer容器</param>
        public void Init()
        {
            if (count == 0)
            {
                lock (objLock)
                {
                    //注册拦截器
                    this.Box.Register(
                    Classes.FromThisAssembly()
                            .IncludeNonPublicTypes()
                            .BasedOn<IInterceptor>()
                            .LifestyleTransient()
                    );
                    //初始化Ioc                    
                    this.PackTheIocBox();
                    //初始化service
                    _appAssembly = AppSetting.Helper.GetAppSettingValue("App.Service", "IocAssemblyName");
                    this.Box.Register(
                    Classes.FromAssemblyNamed(_appAssembly)   //选择Assembly
                            .IncludeNonPublicTypes()                    //约束Type
                            .BasedOn<IServiceBase>()      //约束Type
                            .WithService.DefaultInterfaces()            //匹配类型
                            .LifestyleSingleton()//单例
                    );
                    count++;
                }
            }
        }

        public void PackTheIocBox()
        {
            var types = Assembly.GetAssembly(typeof(IInitIoc)).GetTypes()
                .Where(x => typeof(IInitIoc).IsAssignableFrom(x) && x.IsClass)
                .Where(x => x != typeof(IInitIoc))
                .Where(x => x.GetInterfaces().Count() >= 2)
                .ToList();
            foreach (Type item in types)
            {
                ((IInitIoc)Activator.CreateInstance(item)).InitIocBox();
            }
        }

    }
}
