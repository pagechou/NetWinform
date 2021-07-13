using App.Core.Ioc;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Extends
{
    internal static class WindsorContainerExtensions
    {
        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, InstanceLifeStyle lifeStyle)
        where T : class
        {
            ComponentRegistration<T> componentRegistration;
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

        public static void AutoRegisterExtends(this IWindsorContainer container)
        {
            container.Register(new IRegistration[] { Classes.FromAssembly(typeof(WindsorContainerExtensions).Assembly) });
        }


        public static void RegisterWithInstance(this IWindsorContainer container, Type type, object obj)
        {
            container.Register(new IRegistration[] { Component.For(type).Instance(obj) });
        }
        public static void RegisterWithInstance(this IWindsorContainer container, Type type, object obj, InstanceLifeStyle lifeStyle)
        {
            container.Register(new IRegistration[] { ApplyLifestyle<object>(Component.For(type).Instance(obj), lifeStyle) });
        }


        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration)
        where T : class
        {
            Guid guid = Guid.NewGuid();
            return componentRegistration.Named(guid.ToString()).IsDefault();
        }
        public static void RegisterInterface(this IWindsorContainer container, Type type, Type itype, InstanceLifeStyle lifeStyle)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(itype).ImplementedBy(type), lifeStyle) });
        }

        public static void RegisterInterfaceWithIInterceptor(this IWindsorContainer container, Type type, Type itype, Type interceptorType, InstanceLifeStyle lifeStyle)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(itype).ImplementedBy(type).Interceptors(interceptorType), lifeStyle) });
        }

        public static void RegisterClassAndInterface(this IWindsorContainer container, Type type, Type itype, InstanceLifeStyle lifeStyle)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(itype, type).ImplementedBy(type), lifeStyle) });
        }

        public static void RegisterExtends<T, Timpl>(this IWindsorContainer container)
        where T : class
        where Timpl : T
        {
            container.Register(new IRegistration[] { Component.For<T>().ImplementedBy<Timpl>() });
        }

        public static void RegisterExtends<T>(this IWindsorContainer container, T obj)
        where T : class
        {
            container.Register(new IRegistration[] { Component.For<T>().Instance(obj) });
        }

        public static void RegisterExtends<T>(this IWindsorContainer container, T obj, string name)
        where T : class
        {
            container.Register(new IRegistration[] { Component.For<T>().Instance(obj).Named(name) });
        }



        public static void RegisterExtends(this IWindsorContainer container, Type type, Type implType, string name, InstanceLifeStyle lifeStyle = InstanceLifeStyle.Transient)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(implType).ImplementedBy(type).Named(name), lifeStyle) });
        }

        public static void RegisterMultiple(this IWindsorContainer container, Type type, IEnumerable<Type> implTypes, InstanceLifeStyle lifeStyle = InstanceLifeStyle.Transient)
        {
            foreach (Type implType in implTypes)
            {
                container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(implType).ImplementedBy(type), lifeStyle) });
            }
        }
    }
}