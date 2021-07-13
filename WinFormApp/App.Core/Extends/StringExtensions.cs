using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Extends
{
    /// <summary>
	/// string类型扩展
	/// </summary>
    public static class StringExtensions
    {
        /// <summary>
		/// 判断是否为空。静态方法，与实例无关
		/// </summary> 
		public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
