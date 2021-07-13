using App.Core.Caching;
using App.Core.Ioc;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public static class TestClass
    {
        public static void Cache()
        {

            Export.IocBox.Box.Register(
                    Classes.FromThisAssembly()
                            .IncludeNonPublicTypes()
                            .BasedOn<IInterceptor>()
                            .LifestyleTransient()
                    );
            MemoryCache memory = new MemoryCache();
            memory.InitIocBox();
        }
    }
}

