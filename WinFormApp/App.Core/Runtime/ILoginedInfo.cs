using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Runtime
{
    public interface ILoginedInfo
    {
        /// <summary>
        /// 用户登录Ip地址
        /// </summary>
        string CLoginedIp
        {
            get;
        }

        /// <summary>
        /// 用户登录主机名
        /// </summary>
        string LoginMachine
        {
            get;
        }

        /// <summary>
        /// 用户登录时间
        /// </summary>
        DateTime? DLoginedTime
        {
            get;
        }

        /// <summary>
        /// 只允许一个客户端登录
        /// </summary>
        bool COnlyOneClient
        {
            get;
        }

        string Session
        {
            get;
        }

        /// <summary>
        /// 用户登录时间
        /// </summary>
        DateTime? DSessionUpdateTime
        {
            get;
        }
    }
}