using App.Core.Extends;
using App.Core.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Core
{
    /// <summary>
	/// 应用程序上下文信息
	/// </summary>
	[Serializable]
    public class AppContext
    {
        //private string _local_token;

        public IUserIdentity _local_user { get; set; }

        private static AppContext _instance;

        static int _IsInDesignMode = 0;

        /// <summary>
        /// winfrom和web统一的bin目录地址
        /// </summary>
        public static string BaseDirectory
        {
            get
            {
                string str;
                if (HttpRuntime.AppDomainAppId == null)
                {
                    str = AppDomain.CurrentDomain.BaseDirectory;
                }
                else
                {
                    str = HttpRuntime.BinDirectory;
                }
                return str;
            }
        }

        /// <summary>
        /// 当前上下文实例
        /// </summary>
        public static AppContext Current
        {
            get
            {
                AppContext appContext = _instance;
                if (appContext == null)
                {
                    appContext = new AppContext();
                    _instance = appContext;
                }
                return appContext;
            }
        }

        /// <summary>
        /// 是否处于vs开发或者调试模式
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                if (_IsInDesignMode > 0)
                    return _IsInDesignMode == 1;

                bool flag;
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    using (Process currentProcess = Process.GetCurrentProcess())
                    {
                        flag = currentProcess.ProcessName.ToLowerInvariant().Contains("devenv");
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    _IsInDesignMode = 1;
                }
                else
                {
                    _IsInDesignMode = 2;
                }
                return flag;
            }
        }

        public AppContext()
        {
        }
    }
}