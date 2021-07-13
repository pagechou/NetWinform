using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Runtime
{
    /// <summary>
    /// 用户类型枚举
    /// </summary>
	public enum UserType
    {
        [Description("普通用户")]
        Noraml = 0,
        [Description("普通管理员")]
        Admin = 7,
        [Description("超级管理员")]
        Administrator = 9
    }
}
