using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zhou.Core;
using Zhou.Core.Attributes;
using Zhou.IService;
using Zhou.Models;
using LinqToDB;

namespace Zhou.Service
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