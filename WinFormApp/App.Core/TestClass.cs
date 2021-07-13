using App.Core.Caching;
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
            MemoryCache memory = new MemoryCache();
            memory.InitIocBox();
        }
    }
}

