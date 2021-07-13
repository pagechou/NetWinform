using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Ioc
{
    /// <summary>
    /// 基础注册
    /// </summary>
    public class BasicRegistrar
    {
        /// <summary>
        /// 通过IOC注册指定程序集中实现指定接口的非泛型类
        /// </summary>
        /// <param name="assembly">程序集对象</param>
		public static void RegisterAssembly(Assembly assembly)
        {
            //IocBox.Instance.Box.Register(new IRegistration[] { Classes.FromAssembly(assembly).IncludeNonPublicTypes().BasedOn<ITransientDependency>().If((Type type) => !type.IsGenericTypeDefinition).WithService.Self().WithService.DefaultInterfaces().LifestyleTransient() });
            //IocBox.Instance.Box.Register(new IRegistration[] { Classes.FromAssembly(assembly).IncludeNonPublicTypes().BasedOn<ISingletonDependency>().If((Type type) => !type.IsGenericTypeDefinition).WithService.Self().WithService.DefaultInterfaces().LifestyleSingleton() });
            //IocBox.Instance.Box.Register(new IRegistration[] { Classes.FromAssembly(assembly).IncludeNonPublicTypes().BasedOn<IInterceptor>().If((Type type) => !type.IsGenericTypeDefinition).WithService.Self().LifestyleTransient() });
        }
    }
}
