using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Runtime
{
    public interface IUserIdentity : IIdentity, ILoginedInfo
    {/// <summary>
     /// 用户所属部门
     /// </summary>
        string Department
        {
            get;
        }

        /// <summary>
        /// 用户所属部门名称
        /// </summary>
        string DepartmentDesc
        {
            get;
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        string Password
        {
            get;
        }

        /// <summary>
        /// 用户标识
        /// </summary>
        Int64 UserID
        {
            get;
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        string CUserName
        {
            get;
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        UserType UserType
        {
            get;
        }
        /// <summary>
        /// 当前登录的工厂ID
        /// </summary>
        string CPlantId { get; set; }
        string CBizPlantId { get; set; }
        long CSessionId { get; set; }
        /// <summary>
        /// 支持所在的工厂,默认工厂是CplantId ，这里是所有的工厂
        /// </summary>
        List<string> PlantS { get; set; }
    }
}