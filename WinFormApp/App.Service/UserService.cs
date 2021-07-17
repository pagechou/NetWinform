using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App.Core;
using App.Core.Attributes;
using App.IService;
using App.Models;
using LinqToDB;

namespace App.Service
{
    public class UserService : BaseService, IUserService
    {
        public virtual string GetUserName(string id)
        {

            return DataBase.GetTable<Tb_Users>().FirstOrDefault(x => x.UserId == 6)?.UserName;
        }

        public virtual string GetUserName2(string id)
        {
            ConnStr = "xxx";
            return DataBase.GetTable<Tb_Users>().FirstOrDefault(x => x.UserId == 6)?.UserName;
        }

    }
}