using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Extends
{
    /// <summary>
    /// 反射扩展类
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
		/// 判断子类是否拥有指定的父类（或者父接口）
		/// </summary> 
		public static bool HasParentType(this Type childType, Type parentType)
        {
            return (parentType == null ? false : parentType.IsAssignableFrom(childType));
        }
    }
}
