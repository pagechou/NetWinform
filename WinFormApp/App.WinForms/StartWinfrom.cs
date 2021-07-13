using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core.Ioc;

namespace App.Winforms
{
    public class StartWinfrom
    {
        public static void StartUp()
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
            IocBox.Instance.Init<App.IService.IBaseService>(container);            
        }
    }
}
