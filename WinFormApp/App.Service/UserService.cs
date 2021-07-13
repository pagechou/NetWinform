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
        public virtual int GetUserName(string name)
        {
            return DB.GetTable<Tb_Users>().Where(x=>x.UserId==6)
                .Set(x=>x.Att7,"123")
                .Update();
        }
    }
}