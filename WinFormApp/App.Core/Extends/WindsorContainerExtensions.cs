using App.Core.Ioc;
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
            InstanceLifeStyle lifeStyle1 = lifeStyle;
            if (lifeStyle1 == InstanceLifeStyle.Singleton)
            {
                componentRegistration = registration.LifestyleSingleton();
            }
            else
            {
                componentRegistration = (lifeStyle1 == InstanceLifeStyle.Transient ? registration.LifestyleTransient() : registration);
            }
            return componentRegistration;
        }

        public static void AutoRegister(this IWindsorContainer container)
        {
            container.Register(new IRegistration[] { Classes.FromAssembly(typeof(WindsorContainerExtensions).Assembly) });
        }

        public static ComponentRegistration<T> OverridesExistingRegistration<T>(this ComponentRegistration<T> componentRegistration)
        where T : class
        {
            Guid guid = Guid.NewGuid();
            return componentRegistration.Named(guid.ToString()).IsDefault();
        }
        public static void Register(this IWindsorContainer container, Type type, Type implType, InstanceLifeStyle lifeStyle)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(type).ImplementedBy(implType), lifeStyle) });
        }

        public static void Register<T, Timpl>(this IWindsorContainer container)
        where T : class
        where Timpl : T
        {
            container.Register(new IRegistration[] { Component.For<T>().ImplementedBy<Timpl>() });
        }

        public static void Register<T>(this IWindsorContainer container, object impl)
        where T : class
        {
            container.Register(new IRegistration[] { Component.For<T>().Instance((T)impl) });
        }

        public static void Register<T>(this IWindsorContainer container, object impl, string name)
        where T : class
        {
            container.Register(new IRegistration[] { Component.For<T>().Instance((T)impl).Named(name) });
        }

        public static void Register(this IWindsorContainer container, Type type, object impl)
        {
            container.Register(new IRegistration[] { Component.For(type).Instance(impl) });
        }

        public static void Register(this IWindsorContainer container, Type type, Type implType, string name, InstanceLifeStyle lifeStyle = InstanceLifeStyle.Transient)
        {
            container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(type).ImplementedBy(implType).Named(name), lifeStyle) });
        }

        public static void RegisterMultiple(this IWindsorContainer container, Type type, IEnumerable<Type> implTypes, InstanceLifeStyle lifeStyle = InstanceLifeStyle.Transient)
        {
            foreach (Type implType in implTypes)
            {
                container.Register(new IRegistration[] { WindsorContainerExtensions.ApplyLifestyle<object>(Component.For(type).ImplementedBy(implType), lifeStyle) });
            }
        }
    }
}