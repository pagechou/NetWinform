using App.Core.Extends;
using App.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Init
{
    internal class InitIoc : IInitIoc
    {
        /// <summary>
        /// 将子类实例放入容器
        /// </summary>
        public void InitIocBox()
        {
            Type type = this.GetType();
            Type iType = type.GetInterfaces().FirstOrDefault(x=>x.Name!= "IInitIoc");
            if (iType != null)
            {
                Export.IocBox.Box.Register(type, iType, InstanceLifeStyle.Singleton);
            }
        }
    }
}
