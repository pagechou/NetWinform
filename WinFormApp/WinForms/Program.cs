using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhou.Core.Ioc;

namespace Core
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //加载组件
            InitMoudles();
            Application.Run(new MainForm());
        }

        static void InitMoudles()
        {
            var container = new WindsorContainer();
            IocBox.GeInstance().Init<Zhou.IService.IBaseService>(container);            
        }
    }
}
